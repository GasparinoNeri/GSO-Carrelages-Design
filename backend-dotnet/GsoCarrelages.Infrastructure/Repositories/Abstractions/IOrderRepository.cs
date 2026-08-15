using GsoCarrelages.Infrastructure.Models;

namespace GsoCarrelages.Infrastructure.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<long> CreateAsync(Order order);

    Task<IEnumerable<Order>> GetByClientEmailAsync(string email);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<bool> UpdateStatusAsync(long idCommande, string statut);
}
