namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes ServerlessWorkflow default error types
/// </summary>
public static class ErrorType
{

    const string BaseUri = "https://serverlessworkflow.io/dsl/errors/types";
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for communication errors
    /// </summary>
    public static readonly Uri Communication = new($"{BaseUri}/communication");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for configuration errors
    /// </summary>
    public static readonly Uri Configuration = new($"{BaseUri}/configuration");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for runtime errors
    /// </summary>
    public static readonly Uri Runtime = new($"{BaseUri}/runtime");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for timeout errors
    /// </summary>
    public static readonly Uri Timeout = new($"{BaseUri}/timeout");
    /// <summary>
    /// Gets the default type <see cref="Uri"/> for validation errors
    /// </summary>
    public static readonly Uri Validation = new($"{BaseUri}/validation");

}
