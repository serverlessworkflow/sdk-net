namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowRuntimeBuilder"/> interface
/// </summary>
/// <param name="services">The underlying <see cref="IServiceCollection"/></param>
/// <param name="configuration">The application's <see cref="IConfiguration"/></param>
/// <param name="serviceLifetime">The <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime"/> to use when registering services</param>
public sealed class WorkflowRuntimeBuilder(IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime)
    : IWorkflowRuntimeBuilder
{

    TaskExecutorRegistry? registry;
    CallTaskExecutorRegistry? callRegistry;

    /// <inheritdoc/>
    public IServiceCollection Services { get; } = services;

    /// <inheritdoc/>
    public IConfiguration Configuration { get; } = configuration;

    /// <inheritdoc/>
    public ServiceLifetime ServiceLifetime { get; } = serviceLifetime;

    TaskExecutorRegistry GetOrCreateRegistry()
    {
        if (registry != null) return registry;
        registry = Services.FirstOrDefault(d => d.ServiceType == typeof(TaskExecutorRegistry))?.ImplementationInstance as TaskExecutorRegistry;
        if (registry != null) return registry;
        registry = new TaskExecutorRegistry();
        Services.AddSingleton(registry);
        return registry;
    }

    CallTaskExecutorRegistry GetOrCreateCallRegistry()
    {
        if (callRegistry != null) return callRegistry;
        callRegistry = Services.FirstOrDefault(d => d.ServiceType == typeof(CallTaskExecutorRegistry))?.ImplementationInstance as CallTaskExecutorRegistry;
        if (callRegistry != null) return callRegistry;
        callRegistry = new CallTaskExecutorRegistry();
        Services.AddSingleton(callRegistry);
        return callRegistry;
    }

    WorkflowRuntimeBuilder ReplaceService<TService>(Type implementationType) where TService : class
    {
        Services.Replace(new ServiceDescriptor(typeof(TService), implementationType, ServiceLifetime));
        return this;
    }

    WorkflowRuntimeBuilder ReplaceService<TService>(Func<IServiceProvider, object> factory) where TService : class
    {
        Services.Replace(new ServiceDescriptor(typeof(TService), factory, ServiceLifetime));
        return this;
    }

    WorkflowRuntimeBuilder AddService<TService>(Type implementationType) where TService : class
    {
        Services.Add(new ServiceDescriptor(typeof(TService), implementationType, ServiceLifetime));
        return this;
    }

    WorkflowRuntimeBuilder AddService<TService>(Func<IServiceProvider, object> factory) where TService : class
    {
        Services.Add(new ServiceDescriptor(typeof(TService), factory, ServiceLifetime));
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseAuthenticationHandler<THandler>() where THandler : class, IAuthenticationHandler => ReplaceService<IAuthenticationHandler>(typeof(THandler));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseAuthenticationHandler(Func<IServiceProvider, IAuthenticationHandler> factory) => ReplaceService<IAuthenticationHandler>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseCloudEventBus<TBus>() where TBus : class, ICloudEventBus => ReplaceService<ICloudEventBus>(typeof(TBus));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseCloudEventBus(Func<IServiceProvider, ICloudEventBus> factory) => ReplaceService<ICloudEventBus>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseOAuth2TokenManager<TManager>() where TManager : class, IOAuth2TokenManager => ReplaceService<IOAuth2TokenManager>(typeof(TManager));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseOAuth2TokenManager(Func<IServiceProvider, IOAuth2TokenManager> factory) => ReplaceService<IOAuth2TokenManager>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseContainerRuntime<TRuntime>() where TRuntime : class, IContainerRuntime => ReplaceService<IContainerRuntime>(typeof(TRuntime));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseContainerRuntime(Func<IServiceProvider, IContainerRuntime> factory) => ReplaceService<IContainerRuntime>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseExternalResourceReader<TReader>() where TReader : class, IExternalResourceReader => ReplaceService<IExternalResourceReader>(typeof(TReader));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseExternalResourceReader(Func<IServiceProvider, IExternalResourceReader> factory) => ReplaceService<IExternalResourceReader>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluator<TEvaluator>() where TEvaluator : class, IRuntimeExpressionEvaluator => AddService<IRuntimeExpressionEvaluator>(typeof(TEvaluator));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluator(Func<IServiceProvider, IRuntimeExpressionEvaluator> factory) => AddService<IRuntimeExpressionEvaluator>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluatorProvider<TProvider>() where TProvider : class, IRuntimeExpressionEvaluatorProvider => ReplaceService<IRuntimeExpressionEvaluatorProvider>(typeof(TProvider));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseRuntimeExpressionEvaluatorProvider(Func<IServiceProvider, IRuntimeExpressionEvaluatorProvider> factory) => ReplaceService<IRuntimeExpressionEvaluatorProvider>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSchemaHandler<THandler>() where THandler : class, ISchemaHandler => AddService<ISchemaHandler>(typeof(THandler));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSchemaHandler(Func<IServiceProvider, ISchemaHandler> factory) => AddService<ISchemaHandler>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSchemaHandlerProvider<TProvider>() where TProvider : class, ISchemaHandlerProvider => ReplaceService<ISchemaHandlerProvider>(typeof(TProvider));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSchemaHandlerProvider(Func<IServiceProvider, ISchemaHandlerProvider> factory) => ReplaceService<ISchemaHandlerProvider>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSecretsManager<TManager>() where TManager : class, ISecretsManager => ReplaceService<ISecretsManager>(typeof(TManager));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseSecretsManager(Func<IServiceProvider, ISecretsManager> factory) => ReplaceService<ISecretsManager>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskExecutionContextFactory<TFactory>() where TFactory : class, ITaskExecutionContextFactory => ReplaceService<ITaskExecutionContextFactory>(typeof(TFactory));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskExecutionContextFactory(Func<IServiceProvider, ITaskExecutionContextFactory> factory) => ReplaceService<ITaskExecutionContextFactory>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskExecutor<TDefinition, TExecutor>()
        where TDefinition : TaskDefinition
        where TExecutor : class, ITaskExecutor<TDefinition>
    {
        GetOrCreateRegistry().Register<TDefinition, TExecutor>();
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseCallTaskExecutor<TExecutor>(string callType)
        where TExecutor : class, ITaskExecutor<CallTaskDefinition>
    {
        GetOrCreateCallRegistry().Register<TExecutor>(callType);
        return this;
    }

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskExecutorFactory<TFactory>() where TFactory : class, ITaskExecutorFactory => ReplaceService<ITaskExecutorFactory>(typeof(TFactory));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskExecutorFactory(Func<IServiceProvider, ITaskExecutorFactory> factory) => ReplaceService<ITaskExecutorFactory>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskStateStore<TStore>() where TStore : class, ITaskStateStore => ReplaceService<ITaskStateStore>(typeof(TStore));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseTaskStateStore(Func<IServiceProvider, ITaskStateStore> factory) => ReplaceService<ITaskStateStore>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseWorkflowExecutionContextFactory<TFactory>() where TFactory : class, IWorkflowExecutionContextFactory => ReplaceService<IWorkflowExecutionContextFactory>(typeof(TFactory));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseWorkflowExecutionContextFactory(Func<IServiceProvider, IWorkflowExecutionContextFactory> factory) => ReplaceService<IWorkflowExecutionContextFactory>(factory);

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseWorkflowStateStore<TStore>() where TStore : class, IWorkflowStateStore => ReplaceService<IWorkflowStateStore>(typeof(TStore));

    /// <inheritdoc/>
    public IWorkflowRuntimeBuilder UseWorkflowStateStore(Func<IServiceProvider, IWorkflowStateStore> factory) => ReplaceService<IWorkflowStateStore>(factory);

}
