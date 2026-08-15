using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.IGateways;

public interface IClientGateway
{
    Task<long> CreateAsync(Client client);

    Task<Client?> GetByEmailAsync(string email);
}
