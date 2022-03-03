/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using ProtoBuf.Meta;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using ServerlessWorkflow.Sdk.Services.IO;
using ServerlessWorkflow.Sdk.Services.Validation;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NodeDeserializers;

namespace ServerlessWorkflow.Sdk
{

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
            var newtonsoftJsonDefaultConfig = (JsonSerializerSettings settings) =>
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            };
            var defaultSettings = JsonConvert.DefaultSettings;
            JsonConvert.DefaultSettings = () =>
            {
                var settings = defaultSettings?.Invoke();
                if (settings == null)
                    settings = new();
                newtonsoftJsonDefaultConfig(settings);
                return settings;
            };
            services.AddNewtonsoftJsonSerializer(options =>
            {
                newtonsoftJsonDefaultConfig(options);
            });
            services.AddYamlDotNetSerializer(
                serializer => serializer
                    .IncludeNonPublicProperties()
                    .WithTypeConverter(new OneOfConverter())
                    .WithEmissionPhaseObjectGraphVisitor(args => new ChainedObjectGraphVisitor(args.InnerVisitor)),
                deserializer => deserializer
                    .WithNodeDeserializer(
                        inner => new Iso8601TimeSpanConverter(inner),
                        syntax => syntax.InsteadOf<ScalarNodeDeserializer>())
                    .WithNodeDeserializer(
                        inner => new OneOfDeserializer(inner),
                        syntax => syntax.InsteadOf<Iso8601TimeSpanConverter>()));
            services.AddHttpClient();
            services.AddSingleton<IWorkflowReader, WorkflowReader>();
            services.AddSingleton<IWorkflowWriter, WorkflowWriter>();
            services.AddSingleton<IWorkflowSchemaValidator, WorkflowSchemaValidator>();
            services.AddSingleton<IWorkflowValidator, WorkflowValidator>();
            services.AddTransient<IWorkflowBuilder, WorkflowBuilder>();
            services.AddValidatorsFromAssemblyContaining<WorkflowDefinitionValidator>(ServiceLifetime.Singleton);
            RuntimeTypeModel.Default[typeof(JSchema)].SetSurrogate(typeof(JSchemaSurrogate));
            return services;
        }

    }

}
