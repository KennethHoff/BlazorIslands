namespace BlazorIslands;

public interface IBlazorIslandsFeature
{
    /// <summary>
    /// Gets a read-only set of JavaScript sources to inject into the rendered HTML.
    /// </summary>
    IReadOnlySet<IJavaScriptSource> Sources { get; }

    /// <summary>
    /// Adds a JavaScript source to the set of sources to inject into the rendered HTML.
    /// </summary>
    /// <param name="source">The source to add.</param>
    /// <returns>The current instance of the <see cref="BlazorIslandsFeature"/>.</returns>
    /// <remarks>
    /// Will not add the source if it already exists in the list.
    /// </remarks>
    void AddSource(IJavaScriptSource source);
}
