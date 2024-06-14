using System.Text.Encodings.Web;
using BlazorIslands.Features;
using Microsoft.AspNetCore.Http;

namespace BlazorIslands;

/// <summary>
/// A middleware that injects JavaScript sources into the rendered HTML.
/// </summary>
public sealed class BlazorIslandsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        EnsureFeature(context);

        await next(context);

        await InjectJavaScriptSources(context);
    }

    private static void EnsureFeature(HttpContext context)
    {
        if (context.Features.Get<IJavaScriptSourceFeature>() is not null)
        {
            return;
        }

        var feature = new JavaScriptSourceFeature();
        context.Features.Set<IJavaScriptSourceFeature>(feature);
    }

    private static async Task InjectJavaScriptSources(HttpContext context)
    {
        // if not 200 OK, do not inject the JavaScript sources
        if (context.Response.StatusCode != StatusCodes.Status200OK)
        {
            return;
        }

        if (context.Features.Get<IJavaScriptSourceFeature>() is not { } feature)
        {
            throw new InvalidOperationException(
                "The JavaScriptSourceFeature is not available in the current HTTP context."
            );
        }

        foreach (var source in feature.Sources)
        {
            await source.WriteToAsync(context);
        }
    }
}
