using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.UseCases.Abstractions;

public interface IOrderUseCases
{
    Task<long> CreateAsync(Order order);

    Task<IEnumerable<Order>> GetByClientEmailAsync(string email);

    Task<IEnumerable<Order>> GetAllAsync();

    Task<bool> UpdateStatusAsync(long idCommande, string statut);
}
