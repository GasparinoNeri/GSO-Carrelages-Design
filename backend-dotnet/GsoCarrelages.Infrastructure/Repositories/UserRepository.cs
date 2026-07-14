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

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        const string sql = """
            SELECT
                id_utilisateur AS IdUtilisateur,
                nom AS Nom,
                prenom AS Prenom,
                email AS Email,
                mot_de_passe AS MotDePasse,
                role AS Role,
                actif AS Actif
            FROM utilisateurs
            WHERE email = @Email
              AND mot_de_passe = @Password
              AND actif = 1
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new { Email = email, Password = password }
        );
    }
}