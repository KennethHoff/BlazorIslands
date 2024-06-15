using Microsoft.AspNetCore.Http;

namespace BlazorIslands;

/// <summary>
/// A middleware that injects JavaScript sources into the rendered HTML.
/// </summary>
internal sealed class BlazorIslandsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        if (context.Features.Get<IBlazorIslandsFeature>() is not { } feature)
        {
            return;
        }

        await InjectJavaScriptSources(context, feature);
    }

    private static async Task InjectJavaScriptSources(
        HttpContext context,
        IBlazorIslandsFeature feature
    )
    {
        foreach (var source in feature.Sources)
        {
            await source.WriteToAsync(context);
        }
    }
}
