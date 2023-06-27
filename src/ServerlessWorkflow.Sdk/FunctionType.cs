namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of functions
/// </summary>
public static class FunctionType
{

    /// <summary>
    /// Indicates an Async API function
    /// </summary>
    public const string AsyncApi = "asyncapi";

    /// <summary>
    /// Indicates an expression function
    /// </summary>
    public const string Expression = "expression";

    /// <summary>
    /// Indicates a GraphQL function
    /// </summary>
    public const string GraphQL = "graphql";

    /// <summary>
    /// Indicates an OData function
    /// </summary>
    public const string OData = "odata";

    /// <summary>
    /// Indicates a REST function
    /// </summary>
    public const string Rest = "rest";

    /// <summary>
    /// Indicates an Remote Procedure Call (RPC)
    /// </summary>
    public const string Rpc = "rpc";

    /// <summary>
    /// Gets all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> GetValues()
    {
        yield return Rest;
        yield return Rpc;
        yield return GraphQL;
        yield return OData;
        yield return Expression;
        yield return AsyncApi;
    }

}
