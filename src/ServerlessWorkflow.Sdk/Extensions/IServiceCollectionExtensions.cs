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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using ServerlessWorkflow.Sdk.Services.IO;
using ServerlessWorkflow.Sdk.Services.Serialization;
using ServerlessWorkflow.Sdk.Services.Validation;
using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Defines extensions for <see cref="IServiceCollection"/>s
    /// </summary>
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Adds and configures a <see cref="NewtonsoftJsonSerializer"/> service
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="configurationAction">The <see cref="Action{T}"/> used to configure the <see cref="JsonSerializerSettings"/> used by the <see cref="NewtonsoftJsonSerializer"/></param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddNewtonsoftJsonSerializer(this IServiceCollection services, Action<JsonSerializerSettings> configurationAction)
        {
            services.Configure(configurationAction);
            JsonConvert.DefaultSettings = () => 
            {
                JsonSerializerSettings settings = new();
                configurationAction(settings);
                return settings;
            };
            services.TryAddSingleton<NewtonsoftJsonSerializer>();
            services.AddSingleton<Services.Serialization.ISerializer>(provider => provider.GetRequiredService<NewtonsoftJsonSerializer>());
            services.AddSingleton<IJsonSerializer>(provider => provider.GetRequiredService<NewtonsoftJsonSerializer>());
            return services;
        }

        /// <summary>
        /// Adds and configures a <see cref="NewtonsoftJsonSerializer"/> service
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddNewtonsoftJsonSerializer(this IServiceCollection services)
        {
            services.AddNewtonsoftJsonSerializer(settings => 
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.ContractResolver = new IgnoreEmptyEnumerableContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(true, true, true)
                };
            });
            return services;
        }

        /// <summary>
        /// Adds and configures an YamlDotNet <see cref="ISerializer"/> and <see cref="IDeserializer"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="serializerConfiguration">The <see cref="Action{T}"/> used to configure the <see cref="ISerializer"/> to add</param>
        /// <param name="deserializerConfiguration">The <see cref="Action{T}"/> used to configure the <see cref="IDeserializer"/> to add</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddYamlDotNet(this IServiceCollection services, Action<SerializerBuilder> serializerConfiguration = null, Action<DeserializerBuilder> deserializerConfiguration = null)
        {
            services.TryAddSingleton(provider =>
            {
                SerializerBuilder builder = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance);
                serializerConfiguration?.Invoke(builder);
                return builder.Build();
            });
            services.TryAddSingleton(provider =>
            {
                DeserializerBuilder builder = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .WithNodeDeserializer(
                        inner => new JArrayDeserializer(inner),
                        syntax => syntax.InsteadOf<ArrayNodeDeserializer>())
                    .WithNodeDeserializer(
                        inner => new AbstractTypeDeserializer(inner),
                        syntax => syntax.InsteadOf<ObjectNodeDeserializer>())
                    .WithNodeDeserializer(
                        inner => new JTokenDeserializer(inner),
                        syntax => syntax.InsteadOf<DictionaryNodeDeserializer>())
                    .WithObjectFactory(new NonPublicConstructorObjectFactory())
                    .IncludeNonPublicProperties();
                deserializerConfiguration?.Invoke(builder);
                return builder.Build();
            });
            return services;
        }

        /// <summary>
        /// Adds and configures a <see cref="YamlDotNetSerializer"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <param name="serializerConfiguration">The <see cref="Action{T}"/> used to configure the <see cref="ISerializer"/> to add</param>
        /// <param name="deserializerConfiguration">The <see cref="Action{T}"/> used to configure the <see cref="IDeserializer"/> to add</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddYamlDotNetSerializer(this IServiceCollection services, Action<SerializerBuilder> serializerConfiguration = null, Action<DeserializerBuilder> deserializerConfiguration = null)
        {
            services.AddYamlDotNet(serializerConfiguration, deserializerConfiguration);
            services.TryAddSingleton<YamlDotNetSerializer>();
            services.AddSingleton<Services.Serialization.ISerializer>(provider => provider.GetRequiredService<YamlDotNetSerializer>());
            services.AddSingleton<IYamlSerializer>(provider => provider.GetRequiredService<YamlDotNetSerializer>());
            return services;
        }

        /// <summary>
        /// Adds and configures Serverless Workflow services (<see cref="ISerializer"/>s, <see cref="IWorkflowReader"/>, <see cref="IWorkflowWriter"/>, ...)
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure</param>
        /// <returns>The configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddServerlessWorkflow(this IServiceCollection services)
        {
            services.AddNewtonsoftJsonSerializer();
            services.AddYamlDotNetSerializer(
                serializer => serializer
                    .IncludeNonPublicProperties()
                    .WithTypeConverter(new JTokenSerializer())
                    .WithTypeConverter(new Iso8601TimeSpanSerializer())
                    .WithTypeConverter(new StringEnumSerializer())
                    .WithEmissionPhaseObjectGraphVisitor(args => new ChainedObjectGraphVisitor(args.InnerVisitor)),
                deserializer => deserializer
                    .WithNodeDeserializer(
                        inner => new Iso8601TimeSpanConverter(inner),
                        syntax => syntax.InsteadOf<ScalarNodeDeserializer>()));
            services.AddHttpClient();
            services.AddSingleton<IWorkflowReader, WorkflowReader>();
            services.AddSingleton<IWorkflowWriter, WorkflowWriter>();
            services.AddSingleton<IWorkflowSchemaValidator, WorkflowSchemaValidator>();
            services.AddTransient<IWorkflowBuilder, WorkflowBuilder>();
            services.AddTransient<IValidator<WorkflowDefinition>, WorkflowDefinitionValidator>();
            return services;
        }

    }

}
