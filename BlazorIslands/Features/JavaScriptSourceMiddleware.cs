namespace BlazorIslands.Features;

/// <summary>
/// A middleware that injects JavaScript sources into the rendered HTML.
/// </summary>
public sealed class JavaScriptSourceMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Features.Get<JavaScriptSourceFeature>() is null)
        {
            context.Features.Set(new JavaScriptSourceFeature());
        }

        await next(context);

        Console.WriteLine("Injecting JavaScript sources into the rendered HTML...");

        if (context.Features.Get<JavaScriptSourceFeature>() is { } feature)
        {
            await AddScriptTag(context, feature);
            // await AddInlineScript(context, feature);
        }
    }

    /// <summary>
    /// Adds a script tag for each JavaScript source.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="feature">The <see cref="JavaScriptSourceFeature"/> to extract the sources from.</param>
    private static async Task AddScriptTag(HttpContext context, JavaScriptSourceFeature feature)
    {
        foreach (var source in feature.Sources)
        {
            await context.Response.WriteAsync($"<script src=\"{source.Source}\"></script>");
        }
    }

    /// <summary>
    /// Adds an inline script tag with the content of each JavaScript source.
    /// This is done by loading the content of each source and injecting it into the rendered HTML.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="feature">The <see cref="JavaScriptSourceFeature"/> to extract the sources from.</param>
    /// <exception cref="FileNotFoundException">Thrown when a source file does not exist.</exception>
    private static async Task AddInlineScript(HttpContext context, JavaScriptSourceFeature feature)
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
