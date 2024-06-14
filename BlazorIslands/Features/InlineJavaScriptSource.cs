using Microsoft.AspNetCore.Http;

namespace BlazorIslands.Features;

// TODO: Add support for nonces (OWASP)

public sealed class InlineJavaScriptSource(string source) : IJavaScriptSource
{
    private readonly string _source = source;

    public Task WriteToAsync(HttpContext context)
    {
        return context.Response.WriteAsync($"""<script type="module">{_source}</script>""");
    }

    public bool Equals(IJavaScriptSource? other)
    {
        return other is InlineJavaScriptSource inline && _source == inline._source;
    }

    public override bool Equals(object? obj)
    {
        return obj is InlineJavaScriptSource inline && Equals(inline);
    }

    public override int GetHashCode()
    {
        return _source.GetHashCode();
    }
}
