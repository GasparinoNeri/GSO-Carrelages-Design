using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Core.UseCases.Abstractions;

namespace GsoCarrelages.Core.UseCases;

public class ProductUseCases : IProductUseCases
{
    private readonly IProductGateway _productGateway;

    public ProductUseCases(IProductGateway productGateway)
    {
        _productGateway = productGateway
            ?? throw new ArgumentNullException(nameof(productGateway));
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _productGateway.GetAllAsync();
    }

    public Task<Product?> GetByIdAsync(long id)
    {
        return _productGateway.GetByIdAsync(id);
    }

    public Task<long> CreateAsync(Product product)
    {
        return _productGateway.CreateAsync(product);
    }

    public Task<bool> UpdateAsync(Product product)
    {
        return _productGateway.UpdateAsync(product);
    }

    public Task<bool> DeleteAsync(long id)
    {
        return _productGateway.DeleteAsync(id);
    }
}
