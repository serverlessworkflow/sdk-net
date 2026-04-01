namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes the titles of all default ServerlessWorkflow errors
/// </summary>
public static class ErrorTitle
{

    /// <summary>
    /// Gets the title of communication errors
    /// </summary>
    public const string Communication = "Communication Error";
    /// <summary>
    /// Gets the title of configuration errors
    /// </summary>
    public const string Configuration = "Configuration Error";
    /// <summary>
    /// Gets the title of runtime errors
    /// </summary>
    public const string Runtime = "Runtime Error";
    /// <summary>
    /// Gets the title of timeout errors
    /// </summary>
    public const string Timeout = "Timeout Error";
    /// <summary>
    /// Gets the title of validation errors
    /// </summary>
    public const string Validation = "Validation Error";

}
