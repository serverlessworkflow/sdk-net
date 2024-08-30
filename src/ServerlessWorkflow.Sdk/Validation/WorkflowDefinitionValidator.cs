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
using Json.Schema;
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.Serialization;
using ServerlessWorkflow.Sdk.Models;
using System.Collections.Concurrent;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowDefinitionValidator"/> interface
/// </summary>
/// <param name="httpClient">The <see cref="System.Net.Http.HttpClient"/> used to perform HTTP requests</param>
/// <param name="jsonSerializer">The service used to serialize/deserialize data to/from JSON</param>
/// <param name="yamlSerializer">The service used to serialize/deserialize data to/from YAML</param>
/// <param name="dslValidators">An <see cref="IEnumerable{T}"/> containing all registered <see cref="IValidator"/> used to validate the <see cref="WorkflowDefinition"/> DSL</param>
public class WorkflowDefinitionValidator(HttpClient httpClient, IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer, IEnumerable<IValidator<WorkflowDefinition>> dslValidators)
    : IWorkflowDefinitionValidator
{

    /// <summary>
    /// Gets the <see cref="System.Net.Http.HttpClient"/> used to perform HTTP requests
    /// </summary>
    protected HttpClient HttpClient { get; } = httpClient;

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from YAML
    /// </summary>
    protected IYamlSerializer YamlSerializer { get; } = yamlSerializer;

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing all registered <see cref="IValidator"/> used to validate the <see cref="WorkflowDefinition"/> DSL
    /// </summary>
    protected IEnumerable<IValidator<WorkflowDefinition>> Validators { get; } = dslValidators;

    /// <summary>
    /// Gets a <see cref="ConcurrentDictionary{TKey, TValue}"/> used to cache loaded <see cref="JsonSchema"/>s
    /// </summary>
    protected ConcurrentDictionary<string, JsonSchema> Schemas { get; } = new();

    /// <inheritdoc/>
    public virtual async Task<IValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(workflowDefinition);
        var node = this.JsonSerializer.SerializeToNode(workflowDefinition);
        var schema = await this.GetOrLoadDslSchemaAsync(workflowDefinition.Document.Dsl, cancellationToken).ConfigureAwait(false);
        var schemaValidationOptions = new EvaluationOptions() 
        { 
            OutputFormat = OutputFormat.List 
        };
        var schemaValidationResults = schema.Evaluate(node, schemaValidationOptions);
        if (!schemaValidationResults.IsValid) return new ValidationResult()
        {
            Errors = schemaValidationResults.Details?.Select(d => new ValidationError() 
            { 
                Reference = d.InstanceLocation.ToString(), 
                Details = d.HasErrors && d.Errors?.Count > 0 ? string.Join(Environment.NewLine, d.Errors.Select(e => $"{e.Key}: {e.Value}")) : null 
            }).ToList().AsReadOnly()
        };
        var dslValidationErrors = new List<ValidationError>();
        foreach(var validator in this.Validators)
        {
            var dslValidationResult = await validator.ValidateAsync(workflowDefinition, cancellationToken).ConfigureAwait(false);
            if (!dslValidationResult.IsValid && dslValidationResult.Errors.Count > 0) dslValidationErrors.AddRange(dslValidationResult.Errors.Select(e => new ValidationError()
            {
                Reference = $"/{e.PropertyName.Replace('.', '/')}",
                Details = e.ErrorMessage
            }));
        }
        return new ValidationResult()
        {
            Errors = dslValidationErrors?.ToList().AsReadOnly()
        };
    }

    /// <summary>
    /// Gets or loads the <see cref="JsonSchema"/> of the specified version of the Serverless Workflow DSL 
    /// </summary>
    /// <param name="version">The version of the Serverless Workflow DSL to get the schema of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="JsonSchema"/> of the specified version of the Serverless Workflow DSL </returns>
    protected virtual async Task<JsonSchema> GetOrLoadDslSchemaAsync(string version, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        if (this.Schemas.TryGetValue(version, out var schema)) return schema;
        var uri = new Uri($"https://raw.githubusercontent.com/serverlessworkflow/specification/v{version}/schema/workflow.yaml", UriKind.Absolute);
        var yaml = await this.HttpClient.GetStringAsync(uri, cancellationToken).ConfigureAwait(false);
        var yamlSchema = this.YamlSerializer.Deserialize<object>(yaml);
        var json = this.JsonSerializer.SerializeToText(yamlSchema);
        schema = JsonSchema.FromText(json);
        this.Schemas.TryAdd(version, schema);
        return schema;
    }

    /// <summary>
    /// Creates a new <see cref="IWorkflowDefinitionValidator"/>
    /// </summary>
    /// <returns>A new <see cref="IWorkflowDefinitionValidator"/></returns>
    public static IWorkflowDefinitionValidator Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflowValidation();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowDefinitionValidator>();
    }

}
