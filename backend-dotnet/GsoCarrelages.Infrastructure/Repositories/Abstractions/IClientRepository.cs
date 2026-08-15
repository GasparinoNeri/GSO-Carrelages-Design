using GsoCarrelages.Infrastructure.Models;

namespace GsoCarrelages.Infrastructure.Repositories.Abstractions;

public interface IClientRepository
{
    Task<long> CreateAsync(Client client);

    Task<Client?> GetByEmailAsync(string email);
}
