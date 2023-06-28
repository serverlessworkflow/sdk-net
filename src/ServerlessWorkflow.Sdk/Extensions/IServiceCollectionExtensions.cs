// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
