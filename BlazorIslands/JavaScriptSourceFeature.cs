namespace BlazorIslands;

/// <summary>
/// An HttpContext `Feature` that provides a way to inject JavaScript source code into the rendered HTML.
/// </summary>
internal sealed class JavaScriptSourceFeature : IJavaScriptSourceFeature
{
    private readonly HashSet<IJavaScriptSource> _sources = [];

    public IReadOnlySet<IJavaScriptSource> Sources => _sources;

    public void AddSource(IJavaScriptSource source)
    {
        _sources.Add(source);
    }
}
