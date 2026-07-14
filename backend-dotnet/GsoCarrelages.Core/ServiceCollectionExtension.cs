using GsoCarrelages.Core.UseCases;
using GsoCarrelages.Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GsoCarrelages.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<IAuthUseCases, AuthUseCases>();
        services.AddTransient<IProductUseCases, ProductUseCases>();

        return services;
    }
}
