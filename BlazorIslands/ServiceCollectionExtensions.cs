using Microsoft.Extensions.DependencyInjection;

namespace BlazorIslands;

/// <summary>
/// A collection of extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorIslands(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<BlazorIslandsMiddleware>();
        return services;
    }
}
