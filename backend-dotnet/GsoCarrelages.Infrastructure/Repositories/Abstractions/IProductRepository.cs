using GsoCarrelages.Infrastructure.Models;

namespace GsoCarrelages.Infrastructure.Repositories.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(long id);

    Task<long> CreateAsync(Product product);

    Task<bool> UpdateAsync(Product product);

    Task<bool> DeleteAsync(long id);
}
