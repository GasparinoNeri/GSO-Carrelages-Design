using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using InfrastructureClient = GsoCarrelages.Infrastructure.Models.Client;

namespace GsoCarrelages.Infrastructure.Gateways;

public class ClientGateway : IClientGateway
{
    private readonly IClientRepository _clientRepository;

    public ClientGateway(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository
            ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    public Task<long> CreateAsync(Client client)
    {
        var infrastructureClient = new InfrastructureClient
        {
            Nom = client.Nom,
            Prenom = client.Prenom,
            Email = client.Email,
            Tel = client.Tel,
            Statut = client.Statut
        };

        return _clientRepository.CreateAsync(infrastructureClient);
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        var infrastructureClient =
            await _clientRepository.GetByEmailAsync(email);

        if (infrastructureClient is null)
        {
            return null;
        }

        return new Client
        {
            IdClient = infrastructureClient.IdClient,
            Nom = infrastructureClient.Nom,
            Prenom = infrastructureClient.Prenom,
            Email = infrastructureClient.Email,
            Tel = infrastructureClient.Tel,
            DateInscription = infrastructureClient.DateInscription,
            Statut = infrastructureClient.Statut
        };
    }
}
