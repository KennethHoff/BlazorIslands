namespace BlazorIslands.Features;

/// <summary>
/// Represents a JavaScript source to inject into the rendered HTML.
/// </summary>
/// <param name="source">The URI of the JavaScript source.</param>
public readonly struct JavaScriptSource(string source)
{
    /// <summary>
    /// Gets the JavaScript source.
    /// </summary>
    public string Source { get; } = EnsureLocalUri(source);

    private static string EnsureLocalUri(string source)
    {
        if (Uri.TryCreate(source, UriKind.Absolute, out _))
        {
            throw new ArgumentException("The source must be a relative URI.", nameof(source));
        }

        return source;
    }
}
