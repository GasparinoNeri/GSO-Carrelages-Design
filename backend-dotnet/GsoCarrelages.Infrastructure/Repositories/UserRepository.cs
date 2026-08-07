using Dapper;
using GsoCarrelages.Infrastructure.Data;
using GsoCarrelages.Infrastructure.Models;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;

namespace GsoCarrelages.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public UserRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = """
            SELECT
                id_utilisateur AS IdUtilisateur,
                nom AS Nom,
                prenom AS Prenom,
                email AS Email,
                telephone AS Telephone,
                adresse AS Adresse,
                date_naissance AS DateNaissance,
                photo_profil AS PhotoProfil,
                mot_de_passe AS MotDePasse,
                role AS Role,
                actif AS Actif,
                created_at AS CreatedAt
            FROM utilisateurs
            WHERE email = @Email
              AND actif = 1
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new { Email = email }
        );
    }

    public async Task<long> CreateAsync(User user)
    {
        const string sql = """
            INSERT INTO utilisateurs
            (
                nom,
                prenom,
                email,
                telephone,
                adresse,
                date_naissance,
                photo_profil,
                mot_de_passe,
                role,
                actif
            )
            VALUES
            (
                @Nom,
                @Prenom,
                @Email,
                @Telephone,
                @Adresse,
                @DateNaissance,
                @PhotoProfil,
                @MotDePasse,
                @Role,
                @Actif
            );

            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.ExecuteScalarAsync<long>(sql, user);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        const string sql = """
            SELECT COUNT(*)
            FROM utilisateurs
            WHERE email = @Email
            """;

        using var connection = _connectionFactory.CreateConnection();

        var count = await connection.ExecuteScalarAsync<int>(
            sql,
            new { Email = email }
        );

        return count > 0;
    }

        public async Task<User?> GetByIdAsync(long id)
    {
        const string sql = """
            SELECT
                id_utilisateur AS IdUtilisateur,
                nom AS Nom,
                prenom AS Prenom,
                email AS Email,
                telephone AS Telephone,
                adresse AS Adresse,
                date_naissance AS DateNaissance,
                photo_profil AS PhotoProfil,
                mot_de_passe AS MotDePasse,
                role AS Role,
                actif AS Actif,
                created_at AS CreatedAt
            FROM utilisateurs
            WHERE id_utilisateur = @Id
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new { Id = id }
        );
    }

    public async Task<bool> UpdateAsync(User user)
    {
        const string sql = """
            UPDATE utilisateurs
            SET
                nom = @Nom,
                prenom = @Prenom,
                telephone = @Telephone,
                adresse = @Adresse,
                date_naissance = @DateNaissance,
                photo_profil = @PhotoProfil
            WHERE id_utilisateur = @IdUtilisateur
            """;

        using var connection = _connectionFactory.CreateConnection();

        var rows = await connection.ExecuteAsync(sql, user);

        return rows > 0;
    }
}
