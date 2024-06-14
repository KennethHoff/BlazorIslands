using Microsoft.AspNetCore.Http;

namespace BlazorIslands.Features;

// TODO: Add support for nonces (OWASP)

public sealed class InlineJavaScriptSource(string code) : IJavaScriptSource
{
    private readonly string _code = code;

    public Task WriteToAsync(HttpContext context)
    {
        return context.Response.WriteAsync($"""<script type="module">{_code}</script>""");
    }

    public bool Equals(IJavaScriptSource? other)
    {
        return other is InlineJavaScriptSource inline && _code == inline._code;
    }

    public override bool Equals(object? obj)
    {
        return obj is InlineJavaScriptSource inline && Equals(inline);
    }

    public override int GetHashCode()
    {
        return _code.GetHashCode();
    }
}
