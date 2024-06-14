using BlazorIslands.Features;
using Microsoft.AspNetCore.Http;
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
        services.AddScoped<IJavaScriptSourceFeature>(provider =>
        {
            if (
                provider.GetRequiredService<IHttpContextAccessor>().HttpContext
                is not { } httpContext
            )
            {
                throw new NotSupportedException(
                    "You must use the JavaScriptSourceFeature within an HTTP context."
                );
            }

            if (httpContext.Features.Get<IJavaScriptSourceFeature>() is { } feature)
            {
                return feature;
            }

            feature = new JavaScriptSourceFeature();
            httpContext.Features.Set(feature);
            return feature;
        });
        services.AddSingleton<BlazorIslandsMiddleware>();
        return services;
    }
}
