namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to build and configure <see cref="IWorkflowRuntime"/>s
/// </summary>
public interface IWorkflowRuntimeBuilder
{

    /// <summary>
    /// Gets the underlying <see cref="IServiceCollection"/> used to register runtime services
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// Gets the application's <see cref="IConfiguration"/>
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// Gets the <see cref="ServiceLifetime"/> used to register all runtime services
    /// </summary>
    ServiceLifetime ServiceLifetime { get; }

    /// <summary>
    /// Configures the <see cref="IAuthenticationHandler"/> implementation to use
    /// </summary>
    /// <typeparam name="THandler">The type of <see cref="IAuthenticationHandler"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseAuthenticationHandler<THandler>()
        where THandler : class, IAuthenticationHandler;

    /// <summary>
    /// Configures the <see cref="IAuthenticationHandler"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IAuthenticationHandler"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseAuthenticationHandler(Func<IServiceProvider, IAuthenticationHandler> factory);

    /// <summary>
    /// Configures the <see cref="IAuthenticationHandler"/> implementation to use
    /// </summary>
    /// <typeparam name="TBus">The type of <see cref="ICloudEventBus"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseCloudEventBus<TBus>()
        where TBus : class, ICloudEventBus;

    /// <summary>
    /// Configures the <see cref="ICloudEventBus"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ICloudEventBus"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseCloudEventBus(Func<IServiceProvider, ICloudEventBus> factory);

    /// <summary>
    /// Configures the <see cref="IOAuth2TokenManager"/> implementation to use
    /// </summary>
    /// <typeparam name="TManager">The type of <see cref="IOAuth2TokenManager"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseOAuth2TokenManager<TManager>()
        where TManager : class, IOAuth2TokenManager;

    /// <summary>
    /// Configures the <see cref="IOAuth2TokenManager"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IOAuth2TokenManager"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseOAuth2TokenManager(Func<IServiceProvider, IOAuth2TokenManager> factory);

    /// <summary>
    /// Configures the <see cref="IContainerRuntime"/> implementation to use
    /// </summary>
    /// <typeparam name="TRuntime">The type of <see cref="IContainerRuntime"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseContainerRuntime<TRuntime>()
        where TRuntime : class, IContainerRuntime;

    /// <summary>
    /// Configures the <see cref="IContainerRuntime"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IContainerRuntime"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseContainerRuntime(Func<IServiceProvider, IContainerRuntime> factory);

    /// <summary>
    /// Configures the <see cref="IExternalResourceReader"/> implementation to use
    /// </summary>
    /// <typeparam name="TReader">The type of <see cref="IExternalResourceReader"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseExternalResourceReader<TReader>()
        where TReader : class, IExternalResourceReader;

    /// <summary>
    /// Configures the <see cref="IExternalResourceReader"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IExternalResourceReader"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseExternalResourceReader(Func<IServiceProvider, IExternalResourceReader> factory);

    /// <summary>
    /// Adds an <see cref="IRuntimeExpressionEvaluator"/> implementation
    /// </summary>
    /// <typeparam name="TEvaluator">The type of <see cref="IRuntimeExpressionEvaluator"/> to add</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluator<TEvaluator>()
        where TEvaluator : class, IRuntimeExpressionEvaluator;

    /// <summary>
    /// Adds an <see cref="IRuntimeExpressionEvaluator"/> implementation
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IRuntimeExpressionEvaluator"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluator(Func<IServiceProvider, IRuntimeExpressionEvaluator> factory);

    /// <summary>
    /// Configures the <see cref="IRuntimeExpressionEvaluatorProvider"/> implementation to use
    /// </summary>
    /// <typeparam name="TProvider">The type of <see cref="IRuntimeExpressionEvaluatorProvider"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluatorProvider<TProvider>()
        where TProvider : class, IRuntimeExpressionEvaluatorProvider;

    /// <summary>
    /// Configures the <see cref="IRuntimeExpressionEvaluatorProvider"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IRuntimeExpressionEvaluatorProvider"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluatorProvider(Func<IServiceProvider, IRuntimeExpressionEvaluatorProvider> factory);

    /// <summary>
    /// Adds an <see cref="ISchemaHandler"/> implementation
    /// </summary>
    /// <typeparam name="THandler">The type of <see cref="ISchemaHandler"/> to add</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSchemaHandler<THandler>()
        where THandler : class, ISchemaHandler;

    /// <summary>
    /// Adds an <see cref="ISchemaHandler"/> implementation
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ISchemaHandler"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSchemaHandler(Func<IServiceProvider, ISchemaHandler> factory);

    /// <summary>
    /// Configures the <see cref="ISchemaHandlerProvider"/> implementation to use
    /// </summary>
    /// <typeparam name="TProvider">The type of <see cref="ISchemaHandlerProvider"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSchemaHandlerProvider<TProvider>()
        where TProvider : class, ISchemaHandlerProvider;

    /// <summary>
    /// Configures the <see cref="ISchemaHandlerProvider"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ISchemaHandlerProvider"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSchemaHandlerProvider(Func<IServiceProvider, ISchemaHandlerProvider> factory);

    /// <summary>
    /// Configures the <see cref="ISecretsManager"/> implementation to use
    /// </summary>
    /// <typeparam name="TManager">The type of <see cref="ISecretsManager"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSecretsManager<TManager>()
        where TManager : class, ISecretsManager;

    /// <summary>
    /// Configures the <see cref="ISecretsManager"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ISecretsManager"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseSecretsManager(Func<IServiceProvider, ISecretsManager> factory);

    /// <summary>
    /// Configures the <see cref="IScriptExecutor"/> implementation to use
    /// </summary>
    /// <typeparam name="TExecutor">The type of <see cref="IScriptExecutor"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseScriptExecutor<TExecutor>()
        where TExecutor : class, IScriptExecutor;

    /// <summary>
    /// Configures the <see cref="IScriptExecutor"/> implementation to use
    /// </summary>
    /// <param name="factory">The type of <see cref="IScriptExecutor"/> to use</param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseScriptExecutor(Func<IServiceProvider, IScriptExecutor> factory);

    /// <summary>
    /// Configures the <see cref="IScriptExecutorProvider"/> implementation to use
    /// </summary>
    /// <typeparam name="TProvider">The type of <see cref="IScriptExecutorProvider"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseScriptExecutorProvider<TProvider>()
        where TProvider : class, IScriptExecutorProvider;

    /// <summary>
    /// Configures the <see cref="IScriptExecutorProvider"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IScriptExecutorProvider"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseScriptExecutorProvider(Func<IServiceProvider, IScriptExecutorProvider> factory);

    /// <summary>
    /// Registers a <see cref="ITaskExecutor{TDefinition}"/> for the specified <see cref="TaskDefinition"/> type
    /// </summary>
    /// <typeparam name="TDefinition">The type of <see cref="TaskDefinition"/> handled by the executor</typeparam>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskExecutor<TDefinition, TExecutor>()
        where TDefinition : TaskDefinition
        where TExecutor : class, ITaskExecutor<TDefinition>;

    /// <summary>
    /// Registers a <see cref="ITaskExecutor{TDefinition}"/> for the specified call type (e.g. "http", "openapi", "asyncapi", "grpc")
    /// </summary>
    /// <param name="callType">The call type discriminator to register the executor for</param>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseCallTaskExecutor<TExecutor>(string callType)
        where TExecutor : class, ITaskExecutor<CallTaskDefinition>;

    /// <summary>
    /// Registers a <see cref="ITaskExecutor{TDefinition}"/> for the specified process type (e.g. "container", "shell", "script", "workflow")
    /// </summary>
    /// <param name="processType">The process type discriminator to register the executor for</param>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseRunTaskExecutor<TExecutor>(string processType)
        where TExecutor : class, ITaskExecutor<RunTaskDefinition>;

    /// <summary>
    /// Configures the <see cref="IWorkflowProcessFactory"/> implementation to use
    /// </summary>
    /// <typeparam name="TFactory">The type of <see cref="IWorkflowProcessFactory"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowProcessFactory<TFactory>()
        where TFactory : class, IWorkflowProcessFactory;

    /// <summary>
    /// Configures the <see cref="IWorkflowProcessFactory"/> implementation to use
    /// </summary>
    /// <param name="factory">An <see cref="IWorkflowProcessFactory"/> to use</param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowProcessFactory(Func<IServiceProvider, IWorkflowProcessFactory> factory);

    /// <summary>
    /// Configures the <see cref="ITaskExecutorFactory"/> implementation to use
    /// </summary>
    /// <typeparam name="TFactory">The type of <see cref="ITaskExecutorFactory"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskExecutorFactory<TFactory>()
        where TFactory : class, ITaskExecutorFactory;

    /// <summary>
    /// Configures the <see cref="ITaskExecutorFactory"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ITaskExecutorFactory"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskExecutorFactory(Func<IServiceProvider, ITaskExecutorFactory> factory);

    /// <summary>
    /// Configures the <see cref="IWorkflowExecutionContextFactory"/> implementation to use
    /// </summary>
    /// <typeparam name="TFactory">The type of <see cref="IWorkflowExecutionContextFactory"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowExecutionContextFactory<TFactory>()
        where TFactory : class, IWorkflowExecutionContextFactory;

    /// <summary>
    /// Configures the <see cref="IWorkflowExecutionContextFactory"/> implementation to use
    /// </summary>
    /// <param name="factory">An <see cref="IWorkflowExecutionContextFactory"/> to use</param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowExecutionContextFactory(Func<IServiceProvider, IWorkflowExecutionContextFactory> factory);

    /// <summary>
    /// Configures the <see cref="ITaskExecutionContextFactory"/> implementation to use
    /// </summary>
    /// <typeparam name="TFactory">The type of <see cref="ITaskExecutionContextFactory"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskExecutionContextFactory<TFactory>()
        where TFactory : class, ITaskExecutionContextFactory;

    /// <summary>
    /// Configures the <see cref="ITaskExecutionContextFactory"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ITaskExecutionContextFactory"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskExecutionContextFactory(Func<IServiceProvider, ITaskExecutionContextFactory> factory);

    /// <summary>
    /// Configures the <see cref="IWorkflowDefinitionStore"/> implementation to use
    /// </summary>
    /// <typeparam name="TStore">The type of <see cref="IWorkflowDefinitionStore"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowDefinitionStore<TStore>()
        where TStore : class, IWorkflowDefinitionStore;

    /// <summary>
    /// Configures the <see cref="IWorkflowDefinitionStore"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IWorkflowDefinitionStore"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowDefinitionStore(Func<IServiceProvider, IWorkflowDefinitionStore> factory);

    /// <summary>
    /// Configures the <see cref="IWorkflowStateStore"/> implementation to use
    /// </summary>
    /// <typeparam name="TStore">The type of <see cref="IWorkflowStateStore"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowStateStore<TStore>()
        where TStore : class, IWorkflowStateStore;

    /// <summary>
    /// Configures the <see cref="IWorkflowStateStore"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="IWorkflowStateStore"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseWorkflowStateStore(Func<IServiceProvider, IWorkflowStateStore> factory);

    /// <summary>
    /// Configures the <see cref="ITaskStateStore"/> implementation to use
    /// </summary>
    /// <typeparam name="TStore">The type of <see cref="ITaskStateStore"/> to use</typeparam>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskStateStore<TStore>()
        where TStore : class, ITaskStateStore;

    /// <summary>
    /// Configures the <see cref="ITaskStateStore"/> implementation to use
    /// </summary>
    /// <param name="factory">A factory function used to create the <see cref="ITaskStateStore"/></param>
    /// <returns>The configured <see cref="IWorkflowRuntimeBuilder"/></returns>
    IWorkflowRuntimeBuilder UseTaskStateStore(Func<IServiceProvider, ITaskStateStore> factory);

}
