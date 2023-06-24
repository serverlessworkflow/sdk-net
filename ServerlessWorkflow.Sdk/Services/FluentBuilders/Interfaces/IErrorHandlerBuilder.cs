namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ErrorHandlerDefinition"/>s
/// </summary>
public interface IErrorHandlerBuilder
{

    /// <summary>
    /// Configures the <see cref="RetryDefinition"/> used by 
    /// </summary>
    /// <param name="strategy">The reference name of the <see cref="RetryDefinition"/> to use</param>
    /// <returns></returns>
    IErrorHandlerBuilder UseRetryStrategy(string strategy);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch the specified errors
    /// </summary>
    /// <param name="error">The domain-specific errors to catch</param>
    /// <param name="errorCode">The code of the errors to catch</param>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IStateOutcomeBuilder When(string error, string errorCode);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch the specified errors
    /// </summary>
    /// <param name="error">The domain-specific errors to catch</param>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IStateOutcomeBuilder When(string error);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch any error
    /// </summary>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IStateOutcomeBuilder WhenAny();

    /// <summary>
    /// Builds the <see cref="ErrorHandlerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ErrorHandlerDefinition"/></returns>
    ErrorHandlerDefinition Build();

}
