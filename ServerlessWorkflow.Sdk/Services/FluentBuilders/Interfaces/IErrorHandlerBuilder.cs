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
    IErrorHandlerBuilder Retry(string strategy);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch the specified errors
    /// </summary>
    /// <param name="error">The domain-specific errors to catch</param>
    /// <param name="errorCode">The code of the errors to catch</param>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IErrorHandlerBuilder Catch(string error, string errorCode);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch the specified errors
    /// </summary>
    /// <param name="error">The domain-specific errors to catch</param>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IErrorHandlerBuilder Catch(string error);

    /// <summary>
    /// Configures the <see cref="ErrorHandlerDefinition"/> to catch any error
    /// </summary>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IErrorHandlerBuilder CatchAll();

    /// <summary>
    /// Configures the outcome of handled errors
    /// </summary>
    /// <param name="outcomeSetup">An <see cref="Action{T}"/> used to setup the outcome of handled errors</param>
    /// <returns>The configured <see cref="IStateOutcomeBuilder"/></returns>
    IErrorHandlerBuilder Then(Action<IStateOutcomeBuilder> outcomeSetup);

    /// <summary>
    /// Builds the <see cref="ErrorHandlerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ErrorHandlerDefinition"/></returns>
    ErrorHandlerDefinition Build();

}
