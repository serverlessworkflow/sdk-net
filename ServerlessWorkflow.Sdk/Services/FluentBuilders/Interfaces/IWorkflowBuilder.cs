namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build workflow definitions
/// </summary>
public interface IWorkflowBuilder
    : IMetadataContainerBuilder<IWorkflowBuilder>
{

    /// <summary>
    /// Sets the id of the workflow definition to create
    /// </summary>
    /// <param name="key">The id of the workflow definition to create</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithId(string key);

    /// <summary>
    /// Sets the unique key of the workflow definition to create
    /// </summary>
    /// <param name="key">The unique key of the workflow definition to create</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithKey(string key);

    /// <summary>
    /// Sets the name of the workflow definition to create
    /// </summary>
    /// <param name="name">The name of the workflow definition to create</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithName(string name);

    /// <summary>
    /// Sets the description of the workflow definition to create
    /// </summary>
    /// <param name="description">The description of the workflow definition to create</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithDescription(string description);

    /// <summary>
    /// Sets the version of the workflow definition to create
    /// </summary>
    /// <param name="version">The description of the workflow definition to create</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithVersion(string version);

    /// <summary>
    /// Sets the Serverless Workflow specification version. Defaults to latest
    /// </summary>
    /// <param name="specVersion">The Serverless Workflow specification version</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithSpecVersion(string specVersion);

    /// <summary>
    /// Sets the workflow definition's data input <see cref="JSchema"/> uri
    /// </summary>
    /// <param name="uri">The uri to the data workflow definition's data input <see cref="JSchema"/></param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithDataInputSchema(Uri uri);

    /// <summary>
    /// Sets the workflow definition data input <see cref="JSchema"/>
    /// </summary>
    /// <param name="schema">The workflow definition's <see cref="JSchema"/></param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithDataInputSchema(JsonSchema schema);

    /// <summary>
    /// Annotates the workflow definition to build
    /// </summary>
    /// <param name="annotation">The annotation to append to the workflow definition to build</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithAnnotation(string annotation);

    /// <summary>
    /// Configures the expression language used by the workflow definition to build
    /// </summary>
    /// <param name="language">The expression language to use</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder UseExpressionLanguage(string language);

    /// <summary>
    /// Configures the workflow definition to use the 'jq' expression language
    /// </summary>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder UseJq();

    /// <summary>
    /// Adds the workflow definition authentication definitions defined in the specified file
    /// </summary>
    /// <param name="uri">The uri of the file that defines the authentication definitions</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder ImportAuthenticationDefinitionsFrom(Uri uri);

    /// <summary>
    /// Uses the specified workflow definition's authentication definitions
    /// </summary>
    /// <param name="authenticationDefinitions">An array that contains the workflow definition's authentication definitions</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder UseAuthenticationDefinitions(params AuthenticationDefinition[] authenticationDefinitions);

    /// <summary>
    /// Adds the specified authentication definition to the workflow definition
    /// </summary>
    /// <param name="authenticationDefinition">The authentication definition to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddAuthentication(AuthenticationDefinition authenticationDefinition);

    /// <summary>
    /// Adds a new authentication definition with scheme <see cref="AuthenticationScheme.Basic"/> to the workflow definition
    /// </summary>
    /// <param name="name">The name of the authentication definition to add</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build authentication definition to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction);

    /// <summary>
    /// Adds a new authentication definition with scheme <see cref="AuthenticationScheme.Bearer"/> to the workflow definition
    /// </summary>
    /// <param name="name">The name of the authentication definition to add</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build authentication definition to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction);

    /// <summary>
    /// Adds a new authentication definition with scheme <see cref="AuthenticationScheme.OAuth2"/> to the workflow definition
    /// </summary>
    /// <param name="name">The name of the authentication definition to add</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> used to configure the service used to build authentication definition to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction);

    /// <summary>
    /// Adds the workflow definition constants defined in the specified file
    /// </summary>
    /// <param name="uri">The uri of the file that defines the constants</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder ImportConstantsFrom(Uri uri);

    /// <summary>
    /// Uses the specified workflow definition's constants
    /// </summary>
    /// <param name="constants">An object that represents the workflow definition's constants</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder UseConstants(object constants);

    /// <summary>
    /// Adds the specified constants to the workflow definition
    /// </summary>
    /// <param name="name">The name of the constant to add</param>
    /// <param name="value">The value of the constant to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddConstant(string name, object value);

    /// <summary>
    /// Uses the specified workflow definition secrets
    /// </summary>
    /// <param name="secrets">An <see cref="IEnumerable{T}"/> containing the secrets to use</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder UseSecrets(IEnumerable<string> secrets);

    /// <summary>
    /// Adds the specified secret to the workflow definition
    /// </summary>
    /// <param name="secret">The secret to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddSecret(string secret);

    /// <summary>
    /// Configures the workflow definition's <see cref="WorkflowExecutionTimeoutDefinition"/>
    /// </summary>
    /// <param name="timeoutSetup">An <see cref="Action{T}"/> used to setup the workflow definition's <see cref="WorkflowExecutionTimeoutDefinition"/></param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder WithExecutionTimeout(Action<IWorkflowExecutionTimeoutBuilder> timeoutSetup);

    /// <summary>
    /// Configures the workflow definition to not terminate its execution when there are no active execution paths
    /// </summary>
    /// <param name="keepActive">A boolean indicating whether or not to keep the workflow definition active</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder KeepActive(bool keepActive = true);

    /// <summary>
    /// Sets and configures the startup state definition
    /// </summary>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup state definition</param>
    /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the workflow definition's state definitions</returns>
    IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Sets and configures the startup state definition
    /// </summary>
    /// <param name="name">The name of the startup state definition</param>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup state definition</param>
    /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the workflow definition's state definitions</returns>
    IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Sets and configures the startup state definition
    /// </summary>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup state definition</param>
    /// <param name="scheduleSetup">An <see cref="Action{T}"/> used to setup the workflow definition's schedule</param>
    /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the workflow definition's state definitions</returns>
    IPipelineBuilder StartsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup);

    /// <summary>
    /// Sets and configures the startup state definition
    /// </summary>
    /// <param name="name">The name of the startup state definition</param>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the startup state definition</param>
    /// <param name="scheduleSetup">An <see cref="Action{T}"/> used to setup the workflow definition's schedule</param>
    /// <returns>A new <see cref="IPipelineBuilder"/> used to configure the workflow definition's state definitions</returns>
    IPipelineBuilder StartsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup, Action<IScheduleBuilder> scheduleSetup);

    /// <summary>
    /// Adds the <see cref="EventDefinition"/>s defined in the specified file
    /// </summary>
    /// <param name="uri">The uri of the file that defines the <see cref="EventDefinition"/>s</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder ImportEventsFrom(Uri uri);

    /// <summary>
    /// Adds the specified <see cref="EventDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="e">The <see cref="EventDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddEvent(EventDefinition e);

    /// <summary>
    /// Adds the specified <see cref="EventDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="eventSetup">The <see cref="Action{T}"/> used to setup the <see cref="EventDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddEvent(Action<IEventBuilder> eventSetup);

    /// <summary>
    /// Adds the <see cref="FunctionDefinition"/>s defined in the specified file
    /// </summary>
    /// <param name="uri">The uri of the file that defines the <see cref="FunctionDefinition"/>s</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder ImportFunctionsFrom(Uri uri);

    /// <summary>
    /// Adds the specified <see cref="FunctionDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="functionSetup">The <see cref="Action{T}"/> used to setup the <see cref="FunctionDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddFunction(Action<IFunctionBuilder> functionSetup);

    /// <summary>
    /// Adds the specified <see cref="FunctionDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="function">The <see cref="FunctionDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddFunction(FunctionDefinition function);

    /// <summary>
    /// Adds the <see cref="RetryDefinition"/>s defined in the specified file
    /// </summary>
    /// <param name="uri">The uri of the file that defines the <see cref="RetryDefinition"/>s</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder ImportRetryStrategiesFrom(Uri uri);

    /// <summary>
    /// Adds the specified <see cref="RetryDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="strategy">The <see cref="Action{T}"/> used to setup the <see cref="RetryDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddRetryStrategy(RetryDefinition strategy);

    /// <summary>
    /// Adds the specified <see cref="RetryDefinition"/> to the workflow definition to create
    /// </summary>
    /// <param name="retryStrategySetup">The <see cref="Action{T}"/> used to setup the <see cref="RetryDefinition"/> to add</param>
    /// <returns>The configured <see cref="IWorkflowBuilder"/></returns>
    IWorkflowBuilder AddRetryStrategy(Action<IRetryStrategyBuilder> retryStrategySetup);

    /// <summary>
    /// Builds the workflow definition
    /// </summary>
    /// <returns>A new workflow definition</returns>
    WorkflowDefinition Build();

}
