using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using InfrastructureProduct = GsoCarrelages.Infrastructure.Models.Product;

namespace GsoCarrelages.Infrastructure.Gateways;

public class ProductGateway : IProductGateway
{
    private readonly IProductRepository _productRepository;

    public ProductGateway(IProductRepository productRepository)
    {
        _productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var infrastructureProducts = await _productRepository.GetAllAsync();

        return infrastructureProducts.Select(ToCoreProduct);
    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        var infrastructureProduct = await _productRepository.GetByIdAsync(id);

        return infrastructureProduct is null
            ? null
            : ToCoreProduct(infrastructureProduct);
    }

    public Task<long> CreateAsync(Product product)
    {
        return _productRepository.CreateAsync(ToInfrastructureProduct(product));
    }

    public Task<bool> UpdateAsync(Product product)
    {
        return _productRepository.UpdateAsync(ToInfrastructureProduct(product));
    }

    public Task<bool> DeleteAsync(long id)
    {
        return _productRepository.DeleteAsync(id);
    }

    private static Product ToCoreProduct(InfrastructureProduct product)
    {
        return new Product
        {
            IdProduit = product.IdProduit,
            Nom = product.Nom,
            Description = product.Description,
            PrixUnitaire = product.PrixUnitaire,
            StockOnHand = product.StockOnHand,
            Actif = product.Actif
        };
    }

    private static InfrastructureProduct ToInfrastructureProduct(Product product)
    {
        return new InfrastructureProduct
        {
            IdProduit = product.IdProduit,
            Nom = product.Nom,
            Description = product.Description,
            PrixUnitaire = product.PrixUnitaire,
            StockOnHand = product.StockOnHand,
            Actif = product.Actif
        };
    }
}
