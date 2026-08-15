using Dapper;
using GsoCarrelages.Infrastructure.Data;
using GsoCarrelages.Infrastructure.Models;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;

namespace GsoCarrelages.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public OrderRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<long> CreateAsync(Order order)
    {
        using var connection = _connectionFactory.CreateConnection();

        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string clientSql = """
                SELECT id_client
                FROM clients
                WHERE email = @Email
                LIMIT 1
                """;

            var clientId = await connection.QuerySingleOrDefaultAsync<long?>(
                clientSql,
                new { Email = order.ClientEmail },
                transaction
            );

            if (clientId is null)
            {
                throw new InvalidOperationException(
                    "Le client correspondant à cet utilisateur est introuvable."
                );
            }

            const string localiteSql = """
                SELECT id_localite
                FROM localite
                WHERE localite = @Localite
                  AND code_postal = @CodePostal
                LIMIT 1
                """;

            var localiteId = await connection.QuerySingleOrDefaultAsync<long?>(
                localiteSql,
                new
                {
                    order.Localite,
                    order.CodePostal
                },
                transaction
            );

            if (localiteId is null)
            {
                const string createLocaliteSql = """
                    INSERT INTO localite
                    (
                        localite,
                        code_postal
                    )
                    VALUES
                    (
                        @Localite,
                        @CodePostal
                    );

                    SELECT LAST_INSERT_ID();
                    """;

                localiteId = await connection.ExecuteScalarAsync<long>(
                    createLocaliteSql,
                    new
                    {
                        order.Localite,
                        order.CodePostal
                    },
                    transaction
                );
            }

            const string createAddressSql = """
                INSERT INTO adresses
                (
                    id_client,
                    id_localite,
                    rue,
                    complement,
                    type,
                    contact_nom,
                    contact_tel
                )
                VALUES
                (
                    @IdClient,
                    @IdLocalite,
                    @Rue,
                    @Complement,
                    @Type,
                    @ContactNom,
                    @ContactTel
                );

                SELECT LAST_INSERT_ID();
                """;

            var facturationId = await connection.ExecuteScalarAsync<long>(
                createAddressSql,
                new
                {
                    IdClient = clientId.Value,
                    IdLocalite = localiteId.Value,
                    order.Rue,
                    order.Complement,
                    Type = "facturation",
                    order.ContactNom,
                    order.ContactTel
                },
                transaction
            );

            var livraisonId = await connection.ExecuteScalarAsync<long>(
                createAddressSql,
                new
                {
                    IdClient = clientId.Value,
                    IdLocalite = localiteId.Value,
                    order.Rue,
                    order.Complement,
                    Type = "livraison",
                    order.ContactNom,
                    order.ContactTel
                },
                transaction
            );

            const string orderSql = """
                INSERT INTO commandes
                (
                    id_client,
                    id_adresse_facturation,
                    id_adresse_livraison,
                    statut,
                    total_ttc,
                    devise,
                    lignes_json
                )
                VALUES
                (
                    @IdClient,
                    @IdAdresseFacturation,
                    @IdAdresseLivraison,
                    @Statut,
                    @TotalTtc,
                    @Devise,
                    @LignesJson
                );

                SELECT LAST_INSERT_ID();
                """;

            var orderId = await connection.ExecuteScalarAsync<long>(
                orderSql,
                new
                {
                    IdClient = clientId.Value,
                    IdAdresseFacturation = facturationId,
                    IdAdresseLivraison = livraisonId,
                    order.Statut,
                    order.TotalTtc,
                    order.Devise,
                    order.LignesJson
                },
                transaction
            );

            transaction.Commit();

            return orderId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetByClientEmailAsync(string email)
    {
        const string sql = """
            SELECT
                c.id_commande AS IdCommande,
                c.id_client AS IdClient,
                cl.email AS ClientEmail,
                c.statut AS Statut,
                c.total_ttc AS TotalTtc,
                c.devise AS Devise,
                c.lignes_json AS LignesJson,
                c.created_at AS CreatedAt
            FROM commandes c
            INNER JOIN clients cl
                ON cl.id_client = c.id_client
            WHERE cl.email = @Email
            ORDER BY c.created_at DESC
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryAsync<Order>(
            sql,
            new { Email = email }
        );
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        const string sql = """
            SELECT
                c.id_commande AS IdCommande,
                c.id_client AS IdClient,
                cl.email AS ClientEmail,
                c.statut AS Statut,
                c.total_ttc AS TotalTtc,
                c.devise AS Devise,
                c.lignes_json AS LignesJson,
                c.created_at AS CreatedAt
            FROM commandes c
            INNER JOIN clients cl
                ON cl.id_client = c.id_client
            ORDER BY c.created_at DESC
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryAsync<Order>(sql);
    }

    public async Task<bool> UpdateStatusAsync(
        long idCommande,
        string statut
    )
    {
        const string sql = """
            UPDATE commandes
            SET statut = @Statut
            WHERE id_commande = @IdCommande
            """;

        using var connection = _connectionFactory.CreateConnection();

        var rows = await connection.ExecuteAsync(
            sql,
            new
            {
                IdCommande = idCommande,
                Statut = statut
            }
        );

        return rows > 0;
    }
}
