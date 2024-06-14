using System.IO.Pipelines;
using Microsoft.AspNetCore.Http;

namespace BlazorIslands;

/// <summary>
/// Represents a JavaScript source to inject into the rendered HTML.
/// </summary>
public interface IJavaScriptSource : IEquatable<IJavaScriptSource>
{
    /// <summary>
    /// Writes the JavaScript source to the specified <see cref="PipeWriter"/>.
    /// </summary>
    Task WriteToAsync(HttpContext context);
}
