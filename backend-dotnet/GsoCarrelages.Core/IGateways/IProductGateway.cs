using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.IGateways;

public interface IProductGateway
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(long id);

    Task<long> CreateAsync(Product product);

    Task<bool> UpdateAsync(Product product);

    Task<bool> DeleteAsync(long id);
}
