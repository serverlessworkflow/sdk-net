namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes ServerlessWorkflow default functions
/// </summary>
public static class Function
{

    /// <summary>
    /// The function used to perform an AsyncAPI call
    /// </summary>
    public const string AsyncApi = "asyncapi";
    /// <summary>
    /// The function used to perform a GRPC call
    /// </summary>
    public const string Grpc = "grpc";
    /// <summary>
    /// The function used to perform an HTTP call
    /// </summary>
    public const string Http = "http";
    /// <summary>
    /// The function used to perform an OpenAPI call
    /// </summary>
    public const string OpenApi = "openapi";

    /// <summary>
    /// Enumerates all default functions
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all default functions</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return AsyncApi;
        yield return Grpc;
        yield return Http;
        yield return OpenApi;
    }

}
