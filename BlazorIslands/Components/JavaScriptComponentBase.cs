using BlazorIslands.Features;
using Microsoft.AspNetCore.Components;

namespace BlazorIslands.Components;

public abstract class JavaScriptComponentBase : ComponentBase
{
    [Inject]
    protected IJavaScriptSourceFeature JavaScriptSourceFeature { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        foreach (var javaScriptSource in JavaScriptSources)
        {
            JavaScriptSourceFeature.AddSource(javaScriptSource);
        }
    }

    /// <summary>
    /// Defines which JavaScript sources to add to the rendered HTML.
    /// </summary>
    protected abstract IEnumerable<JavaScriptSource> JavaScriptSources { get; }
}
