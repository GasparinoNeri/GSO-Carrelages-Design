using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.IGateways;

public interface IOrderGateway
{
    Task<long> CreateAsync(Order order);

    Task<IEnumerable<Order>> GetByClientEmailAsync(string email);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<bool> UpdateStatusAsync(long idCommande, string statut);
}
