using Dapper;
using GsoCarrelages.Infrastructure.Models;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using GsoCarrelages.Infrastructure.Data;

namespace GsoCarrelages.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public ProductRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        const string sql = """
            SELECT 
                id_produit AS IdProduit,
                nom AS Nom,
                description AS Description,
                prix_unitaire AS PrixUnitaire,
                stock_on_hand AS StockOnHand,
                actif AS Actif
            FROM produits
            WHERE actif = 1
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        const string sql = """
            SELECT 
                id_produit AS IdProduit,
                nom AS Nom,
                description AS Description,
                prix_unitaire AS PrixUnitaire,
                stock_on_hand AS StockOnHand,
                actif AS Actif
            FROM produits
            WHERE id_produit = @Id
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<long> CreateAsync(Product product)
{
    const string sql = """
        INSERT INTO produits
        (
            nom,
            description,
            id_categorie,
            id_fournisseur,
            prix_unitaire,
            stock_on_hand,
            stock_reserved,
            actif
        )
        VALUES
        (
            @Nom,
            @Description,
            1,
            1,
            @PrixUnitaire,
            @StockOnHand,
            0,
            @Actif
        );

        SELECT LAST_INSERT_ID();
        """;

    using var connection = _connectionFactory.CreateConnection();

    return await connection.ExecuteScalarAsync<long>(sql, product);
}

    public async Task<bool> UpdateAsync(Product product)
    {
        const string sql = """
            UPDATE produits
            SET
                nom = @Nom,
                description = @Description,
                prix_unitaire = @PrixUnitaire,
                stock_on_hand = @StockOnHand,
                actif = @Actif
            WHERE id_produit = @IdProduit
            """;

        using var connection = _connectionFactory.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(sql, product);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        const string sql = """
            DELETE FROM produits
            WHERE id_produit = @Id
            """;

        using var connection = _connectionFactory.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

        return rowsAffected > 0;
    }
}