namespace BlazorIslands.Features;

/// <summary>
/// Represents a JavaScript source to inject into the rendered HTML.
/// </summary>
/// <param name="source">The URI of the JavaScript source.</param>
public sealed class JavaScriptSource(string source) : IEquatable<JavaScriptSource>
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

    public bool Equals(JavaScriptSource? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Source == other.Source;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is JavaScriptSource other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Source.GetHashCode();
    }
}
