namespace BlazorIslands.Features;

/// <summary>
/// An HttpContext `Feature` that provides a way to inject JavaScript source code into the rendered HTML.
/// </summary>
internal sealed class JavaScriptSourceFeature : IJavaScriptSourceFeature
{
    private readonly HashSet<JavaScriptSource> _sources = [];

    public IReadOnlySet<JavaScriptSource> Sources => _sources;

    public void AddSource(JavaScriptSource source) => _sources.Add(source);
}

public interface IJavaScriptSourceFeature
{
    /// <summary>
    /// Gets a read-only set of JavaScript sources to inject into the rendered HTML.
    /// </summary>
    IReadOnlySet<JavaScriptSource> Sources { get; }

    /// <summary>
    /// Adds a JavaScript source to the set of sources to inject into the rendered HTML.
    /// </summary>
    /// <param name="source">The source to add.</param>
    /// <returns>The current instance of the <see cref="JavaScriptSourceFeature"/>.</returns>
    /// <remarks>
    /// Will not add the source if it already exists in the list.
    /// </remarks>
    void AddSource(JavaScriptSource source);
}
