using Dapper;
using GsoCarrelages.Infrastructure.Data;
using GsoCarrelages.Infrastructure.Models;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;

namespace GsoCarrelages.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public ClientRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<long> CreateAsync(Client client)
    {
        const string sql = """
            INSERT INTO clients
            (
                nom,
                prenom,
                email,
                tel,
                statut
            )
            VALUES
            (
                @Nom,
                @Prenom,
                @Email,
                @Tel,
                @Statut
            );

            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.ExecuteScalarAsync<long>(sql, client);
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        const string sql = """
            SELECT
                id_client AS IdClient,
                nom AS Nom,
                prenom AS Prenom,
                email AS Email,
                tel AS Tel,
                date_inscription AS DateInscription,
                statut AS Statut
            FROM clients
            WHERE email = @Email
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Client>(
            sql,
            new { Email = email }
        );
    }
}
