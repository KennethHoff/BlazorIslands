using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace BlazorIslands;

public abstract class JavaScriptComponentBase : ComponentBase
{
    [Inject]
    protected IHttpContextAccessor HttpContextAccessor { get; init; } = default!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var feature = HttpContextAccessor.HttpContext!.Features.Get<IJavaScriptSourceFeature>()!;
        foreach (var javaScriptSource in JavaScriptSources)
        {
            feature.AddSource(javaScriptSource);
        }
    }

    /// <summary>
    /// Defines which JavaScript sources to add to the rendered HTML.
    /// </summary>
    protected abstract IEnumerable<IJavaScriptSource> JavaScriptSources { get; }
}
