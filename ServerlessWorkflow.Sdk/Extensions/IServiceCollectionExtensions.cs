using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using ServerlessWorkflow.Sdk.Services.IO;
using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures Serverless Workflow services (<see cref="Neuroglia.Serialization.ISerializer"/>s, <see cref="IWorkflowReader"/>, <see cref="IWorkflowWriter"/>, ...)
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddServerlessWorkflow(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<IWorkflowExternalDefinitionResolver, WorkflowExternalDefinitionResolver>();
        services.AddSingleton<IWorkflowReader, WorkflowReader>();
        services.AddSingleton<IWorkflowWriter, WorkflowWriter>();
        services.AddSingleton<IWorkflowSchemaValidator, WorkflowSchemaValidator>();
        services.AddSingleton<IWorkflowValidator, WorkflowValidator>();
        services.AddTransient<IWorkflowBuilder, WorkflowBuilder>();
        services.AddValidatorsFromAssemblyContaining<WorkflowDefinitionValidator>(ServiceLifetime.Singleton);
        return services;
    }

}
