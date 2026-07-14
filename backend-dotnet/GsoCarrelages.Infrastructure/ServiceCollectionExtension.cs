using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Infrastructure.Gateways;
using GsoCarrelages.Infrastructure.Repositories;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GsoCarrelages.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IUserGateway, UserGateway>();
        services.AddTransient<IProductGateway, ProductGateway>();

        return services;
    }
}
