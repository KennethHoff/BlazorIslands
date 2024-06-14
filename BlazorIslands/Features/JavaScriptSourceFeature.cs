namespace BlazorIslands.Features;

/// <summary>
/// An HttpContext `Feature` that provides a way to inject JavaScript source code into the rendered HTML.
/// </summary>
public sealed class JavaScriptSourceFeature
{
    private readonly HashSet<JavaScriptSource> _sources = [];

    /// <summary>
    /// Gets a read-only set of JavaScript sources to inject into the rendered HTML.
    /// </summary>
    public IReadOnlySet<JavaScriptSource> Sources => _sources;

    /// <summary>
    /// Adds a JavaScript source to the set of sources to inject into the rendered HTML.
    /// </summary>
    /// <param name="source">The source to add.</param>
    /// <returns>The current instance of the <see cref="JavaScriptSourceFeature"/>.</returns>
    /// <remarks>
    /// Will not add the source if it already exists in the list.
    /// </remarks>
    public JavaScriptSourceFeature AddSource(JavaScriptSource source)
    {
        _sources.Add(source);
        return this;
    }
}

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
