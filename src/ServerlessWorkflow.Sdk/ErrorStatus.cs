namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes all default ServerlessWorkflow error statuses
/// </summary>
public static class ErrorStatus
{

    /// <summary>
    /// Gets the default status for all configuration errors
    /// </summary>
    public const int Configuration = 400;
    /// <summary>
    /// Gets the default status for all validation errors
    /// </summary>
    public const int Validation = 400;
    /// <summary>
    /// Gets the default status for all runtime expression errors
    /// </summary>
    public const int RuntimeExpression = 400;
    /// <summary>
    /// Gets the default status for all authentication errors
    /// </summary>
    public const int Authentication = 401;
    /// <summary>
    /// Gets the default status for all authorization errors
    /// </summary>
    public const int Authorization = 403;
    /// <summary>
    /// Gets the default status for all timeout errors
    /// </summary>
    public const int Timeout = 408;
    /// <summary>
    /// Gets the default status for all communication errors
    /// </summary>
    public const int Communication = 500;
    /// <summary>
    /// Gets the default status for all runtime errors
    /// </summary>
    public const int Runtime = 500;

}
