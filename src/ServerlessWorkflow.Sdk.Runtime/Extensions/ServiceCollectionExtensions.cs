#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures a Serverless Workflow runtime with default services
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <param name="configuration">The application's <see cref="IConfiguration"/></param>
    /// <param name="configure">An <see cref="Action{T}"/> used to configure the <see cref="IWorkflowRuntimeBuilder"/></param>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> to use for all runtime services. Defaults to <see cref="ServiceLifetime.Singleton"/></param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddServerlessWorkflowRuntime(this IServiceCollection services, IConfiguration configuration, Action<IWorkflowRuntimeBuilder>? configure = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var builder = new WorkflowRuntimeBuilder(services, configuration, lifetime);
        services.AddMemoryCache();
        services.AddHttpClient();
        RegisterDefaultServices(builder);
        RegisterDefaultTaskExecutors(builder);
        RegisterDefaultCallTaskExecutors(builder);
        RegisterDefaultRunTaskExecutors(builder);
        configure?.Invoke(builder);
        return services;
    }

    static void RegisterDefaultServices(WorkflowRuntimeBuilder builder)
    {
        builder.UseAuthenticationHandler<AuthenticationHandler>();
        builder.UseCloudEventBus<InMemoryCloudEventBus>();
        builder.UseOAuth2TokenManager<OAuth2TokenManager>();
        builder.UseExternalResourceReader<ExternalResourceReader>();
        builder.UseSecretsManager<SecretsManager>();
        builder.UseRuntimeExpressionEvaluator<JQRuntimeExpressionEvaluator>();
        builder.UseRuntimeExpressionEvaluator<JSRuntimeExpressionEvaluator>();
        builder.UseRuntimeExpressionEvaluatorProvider<RuntimeExpressionEvaluatorProvider>();
        builder.UseSchemaHandler<JsonSchemaHandler>();
        builder.UseSchemaHandler<AvroSchemaHandler>();
        builder.UseSchemaHandler<XmlSchemaHandler>();
        builder.UseSchemaHandlerProvider<SchemaHandlerProvider>();
        builder.UseScriptExecutor<NodeJSScriptExecutor>();
        builder.UseScriptExecutor<PythonScriptExecutor>();
        builder.UseScriptExecutorProvider<ScriptExecutorProvider>();
        builder.UseTaskExecutorFactory<TaskExecutorFactory>();
        builder.UseTaskStateStore<InMemoryTaskStateStore>();
        builder.UseWorkflowStateStore<InMemoryWorkflowStateStore>();
    }

    static void RegisterDefaultTaskExecutors(WorkflowRuntimeBuilder builder)
    {
        builder.UseTaskExecutor<DoTaskDefinition, DoTaskExecutor>();
        builder.UseTaskExecutor<EmitTaskDefinition, EmitTaskExecutor>();
        builder.UseTaskExecutor<ExtensionTaskDefinition, ExtensionTaskExecutor>();
        builder.UseTaskExecutor<ForTaskDefinition, ForTaskExecutor>();
        builder.UseTaskExecutor<ForkTaskDefinition, ForkTaskExecutor>();
        builder.UseTaskExecutor<ListenTaskDefinition, ListenTaskExecutor>();
        builder.UseTaskExecutor<RaiseTaskDefinition, RaiseTaskExecutor>();
        builder.UseTaskExecutor<SetTaskDefinition, SetTaskExecutor>();
        builder.UseTaskExecutor<SwitchTaskDefinition, SwitchTaskExecutor>();
        builder.UseTaskExecutor<TryTaskDefinition, TryTaskExecutor>();
        builder.UseTaskExecutor<WaitTaskDefinition, WaitTaskExecutor>();
    }

    static void RegisterDefaultCallTaskExecutors(WorkflowRuntimeBuilder builder)
    {
        builder.UseCallTaskExecutor<HttpCallTaskExecutor>(ServerlessWorkflow.Sdk.Function.Http);
        builder.UseCallTaskExecutor<OpenApiCallTaskExecutor>(ServerlessWorkflow.Sdk.Function.OpenApi);
        builder.UseCallTaskExecutor<AsyncApiCallTaskExecutor>(ServerlessWorkflow.Sdk.Function.AsyncApi);
        builder.UseCallTaskExecutor<GrpcCallTaskExecutor>(ServerlessWorkflow.Sdk.Function.Grpc);
    }

    static void RegisterDefaultRunTaskExecutors(WorkflowRuntimeBuilder builder)
    {
        builder.UseRunTaskExecutor<ContainerRunTaskExecutor>(ServerlessWorkflow.Sdk.ProcessType.Container);
        builder.UseRunTaskExecutor<ShellRunTaskExecutor>(ServerlessWorkflow.Sdk.ProcessType.Shell);
        builder.UseRunTaskExecutor<ScriptRunTaskExecutor>(ServerlessWorkflow.Sdk.ProcessType.Script);
        builder.UseRunTaskExecutor<WorkflowRunTaskExecutor>(ServerlessWorkflow.Sdk.ProcessType.Workflow);
    }

}
