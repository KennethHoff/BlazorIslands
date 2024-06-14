using Microsoft.AspNetCore.Http;

namespace BlazorIslands.Features;

/// <summary>
/// Represents an external JavaScript source to inject into the rendered HTML.
/// </summary>
/// <param name="source">The URI of the JavaScript source. Has to be a relative URI.</param>
public sealed class ExternalJavaScriptSource(string source) : IJavaScriptSource
{
    private readonly string _source = EnsureLocalUri(source);

    private static string EnsureLocalUri(string source)
    {
        if (Uri.TryCreate(source, UriKind.Absolute, out _))
        {
            throw new ArgumentException("The source must be a relative URI.", nameof(source));
        }

        return source;
    }

    public Task WriteToAsync(HttpContext context)
    {
        return context.Response.WriteAsync($"""<script src="{_source}" type="module"></script>""");
    }

    public bool Equals(ExternalJavaScriptSource? other)
    {
        return other is not null && _source == other._source;
    }

    public bool Equals(IJavaScriptSource? other)
    {
        return other is ExternalJavaScriptSource external && Equals(external);
    }

    public override bool Equals(object? obj)
    {
        return obj is ExternalJavaScriptSource external && Equals(external);
    }

    public override int GetHashCode()
    {
        return _source.GetHashCode();
    }
}
