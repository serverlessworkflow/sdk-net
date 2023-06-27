using Json.More;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="JsonElement"/>s
/// </summary>
public static class JsonElementExtensions
{

    /// <summary>
    /// Unwraps the <see cref="JsonElement"/> into a new, non-JSON value
    /// </summary>
    /// <param name="jsonElement">The <see cref="JsonElement"/> to unwrap</param>
    /// <returns>The unwrapped value</returns>
    public static object? ToObject(this JsonElement jsonElement) => jsonElement.AsNode()?.ToObject();

}
