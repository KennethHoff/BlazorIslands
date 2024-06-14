namespace BlazorIslands.Features;

/// <summary>
/// An HttpContext `Feature` that provides a way to inject JavaScript source code into the rendered HTML.
/// </summary>
internal sealed class JavaScriptSourceFeature : IJavaScriptSourceFeature
{
    private readonly HashSet<JavaScriptSource> _sources = [];

    public IReadOnlySet<JavaScriptSource> Sources => _sources;

    public void AddSource(JavaScriptSource source)
    {
        _sources.Add(source);
    }
}
