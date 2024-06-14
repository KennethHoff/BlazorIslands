using BlazorIslands.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorIslands;

/// <summary>
/// A middleware that injects JavaScript sources into the rendered HTML.
/// </summary>
public sealed class BlazorIslandsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        if (context.RequestServices.GetRequiredService<IJavaScriptSourceFeature>() is { } feature)
        {
            await AddScriptTag(context, feature);
            // await AddInlineScript(context, feature);
        }
    }

    /// <summary>
    /// Adds a script tag for each JavaScript source.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="feature">The <see cref="IJavaScriptSourceFeature"/> to extract the sources from.</param>
    private static async Task AddScriptTag(HttpContext context, IJavaScriptSourceFeature feature)
    {
        foreach (var source in feature.Sources)
        {
            await context.Response.WriteAsync($"<script src=\"{source.Source}\" defer></script>");
        }
    }

    /// <summary>
    /// Adds an inline script tag with the content of each JavaScript source.
    /// This is done by loading the content of each source and injecting it into the rendered HTML.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="feature">The <see cref="IJavaScriptSourceFeature"/> to extract the sources from.</param>
    /// <exception cref="FileNotFoundException">Thrown when a source file does not exist.</exception>
    /// <remarks>
    /// Doesn't really work.. lol
    /// </remarks>
    private static async Task AddInlineScript(HttpContext context, IJavaScriptSourceFeature feature)
    {
        // Load the content of all the sources
        var content = await Task.WhenAll(
            feature.Sources.Select(async source =>
            {
                var path = Path.Combine(
                    context.Request.PathBase,
                    $"wwwroot/{source.Source.TrimStart('/')}"
                );
                var file = new FileInfo(path);
                if (!file.Exists)
                {
                    throw new FileNotFoundException($"The file '{file.FullName}' does not exist.");
                }

                return await File.ReadAllTextAsync(file.FullName);
            })
        );

        // Inject the content into the rendered HTML
        await context.Response.WriteAsync($"<script>{string.Join("\n", content)}</script>");
    }
}
