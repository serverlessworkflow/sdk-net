// Copyright © 2024-Present The Serverless Workflow Specification Authors
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
using Neuroglia.Serialization;
using Neuroglia.Serialization.Yaml;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Serialization.Yaml;
using ServerlessWorkflow.Sdk.Validation;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="IServiceCollection"/>s
/// </summary>
public static class IServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures ServerlessWorkflow IO services
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
    /// <returns>The configured <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddServerlessWorkflowValidation(this IServiceCollection services) 
    {
        services.AddHttpClient();
        services.AddJsonSerializer();
        services.AddYamlDotNetSerializer(options =>
        {
            YamlSerializer.DefaultSerializerConfiguration(options.Serializer);
            YamlSerializer.DefaultDeserializerConfiguration(options.Deserializer);
            options.Deserializer.WithNodeDeserializer(
                inner => new TaskDefinitionYamlDeserializer(inner),
                syntax => syntax.InsteadOf<JsonSchemaDeserializer>());
            options.Deserializer.WithNodeDeserializer(
                inner => new OneOfNodeDeserializer(inner),
                syntax => syntax.InsteadOf<TaskDefinitionYamlDeserializer>());
            options.Deserializer.WithNodeDeserializer(
               inner => new OneOfScalarDeserializer(inner),
               syntax => syntax.InsteadOf<StringEnumDeserializer>());
            var mapEntryConverter = new MapEntryYamlConverter(() => options.Serializer.Build(), () => options.Deserializer.Build());
            options.Deserializer.WithTypeConverter(mapEntryConverter);
            options.Serializer.WithTypeConverter(mapEntryConverter);
            options.Serializer.WithTypeConverter(new OneOfConverter());
        });
        services.AddSingleton<IWorkflowDefinitionValidator, WorkflowDefinitionValidator>();
        services.AddValidatorsFromAssemblyContaining<WorkflowDefinition>();
        var defaultPropertyNameResolver = ValidatorOptions.Global.PropertyNameResolver;
        ValidatorOptions.Global.PropertyNameResolver = (type, member, lambda) => member == null ? defaultPropertyNameResolver(type, member, lambda) : member.Name.ToCamelCase();
        return services;
    }

}
