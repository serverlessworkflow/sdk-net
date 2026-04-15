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

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute custom function <see cref="CallTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="httpClientFactory">The service used to create <see cref="HttpClient"/>s</param>
/// <param name="authenticationHandler">The service used to handle authentication policies</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class CustomFunctionCallTaskExecutor(IServiceProvider serviceProvider, ILogger<CustomFunctionCallTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IHttpClientFactory httpClientFactory, IAuthenticationHandler authenticationHandler, ITaskExecutionContext<CallTaskDefinition> task)
    : TaskExecutor<CallTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    const string CustomFunctionDefinitionFile = "function.yaml";
    const string GitHubHost = "github.com";
    const string GitLabHost = "gitlab";

    TaskDefinition? function;

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        if (Task.Workflow.Definition.Use?.Functions?.TryGetValue(Task.Definition.Call, out var fn) == true && fn != null) function = fn;
        else if (Uri.TryCreate(Task.Definition.Call, UriKind.Absolute, out var uri) && (uri.IsFile || !string.IsNullOrWhiteSpace(uri.Host))) function = await GetCustomFunctionAsync(new EndpointDefinition { Uri = uri }, cancellationToken).ConfigureAwait(false);
        else if (Task.Definition.Call.Contains('@'))
        {
            var components = Task.Definition.Call.Split('@', StringSplitOptions.RemoveEmptyEntries);
            if (components.Length != 2) throw new NotSupportedException($"Unknown/unsupported function '{Task.Definition.Call}'");
            function = await GetCustomFunctionFromCatalogAsync(components[0], components[1], cancellationToken).ConfigureAwait(false);
        }
        else if (Task.Definition.Call.Contains(':'))
        {
            var components = Task.Definition.Call.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (components.Length != 2) throw new Exception($"The specified value '{Task.Definition.Call}' is not a valid custom function qualified name ({{name}}:{{version}})");
            var functionName = components[0];
            var functionVersion = components[1];
            uri = new Uri($"https://github.com/serverlessworkflow/catalog/tree/main/functions/{functionName}/{functionVersion}/{CustomFunctionDefinitionFile}");
            function = await GetCustomFunctionAsync(new EndpointDefinition { Uri = uri }, cancellationToken).ConfigureAwait(false);
        }
        else throw new NotSupportedException($"Unknown/unsupported function '{Task.Definition.Call}'");
    }

    async Task<TaskDefinition> GetCustomFunctionAsync(EndpointDefinition endpoint, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(endpoint);
        var uri = endpoint.Uri;
        if (!uri.OriginalString.EndsWith(CustomFunctionDefinitionFile)) uri = new Uri(uri, CustomFunctionDefinitionFile);
        if (uri.Host.Equals(GitHubHost, StringComparison.OrdinalIgnoreCase)) uri = TransformGithubUriToRawUri(uri);
        else if (uri.Host.Contains(GitLabHost)) uri = TransformGitlabUriToRawUri(uri);
        using var httpClient = httpClientFactory.CreateClient();
        if (endpoint.Authentication != null)
        {
            var authResult = await authenticationHandler.HandleAsync(endpoint.Authentication, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authResult.Scheme, authResult.Value);
        }
        try
        {
            using var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var yaml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            var serializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var functionDef = serializer.Deserialize<Dictionary<string, object>>(yaml);
            var json = JsonSerializer.Serialize(functionDef);
            return JsonSerializer.Deserialize<TaskDefinition>(json) ?? throw new InvalidOperationException($"Failed to deserialize custom function definition from '{uri}'");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load the custom function defined at '{uri}': {ex.Message}", ex);
        }
    }

    async Task<TaskDefinition> GetCustomFunctionFromCatalogAsync(string functionName, string catalogName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(functionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(catalogName);
        var components = functionName.Split(':', StringSplitOptions.RemoveEmptyEntries);
        if (components.Length != 2) throw new Exception($"The specified value '{functionName}' is not a valid custom function qualified name ({{name}}:{{version}})");
        var name = components[0];
        var version = components[1];
        if (Task.Workflow.Definition.Use?.Catalogs?.TryGetValue(catalogName, out var catalog) != true || catalog == null) throw new NullReferenceException($"Failed to find a catalog with the specified name '{catalogName}'");
        var catalogUri = catalog.Endpoint.Match(
            endpoint => endpoint.Uri,
            uri => uri
        );
        var catalogAuthentication = catalog.Endpoint.Match(
            endpoint => endpoint.Authentication,
            _ => (AuthenticationPolicyDefinition?)null
        );
        return await GetCustomFunctionAsync(new EndpointDefinition
        {
            Uri = new Uri(catalogUri, $"/functions/{name}/{version}"),
            Authentication = catalogAuthentication
        }, cancellationToken).ConfigureAwait(false);
    }

    static Uri TransformGithubUriToRawUri(Uri uri)
    {
        if (!uri.Host.Equals(GitHubHost, StringComparison.OrdinalIgnoreCase)) return uri;
        var rawUri = uri.AbsoluteUri.Replace(GitHubHost, "raw.githubusercontent.com", StringComparison.OrdinalIgnoreCase);
        rawUri = rawUri.Replace("/tree/", "/refs/heads/", StringComparison.OrdinalIgnoreCase);
        return new(rawUri, UriKind.Absolute);
    }

    static Uri TransformGitlabUriToRawUri(Uri uri)
    {
        if (!uri.Host.Contains(GitLabHost, StringComparison.OrdinalIgnoreCase)) return uri;
        var rawUri = uri.AbsoluteUri.Replace("/-/blob/", "/-/raw/", StringComparison.OrdinalIgnoreCase);
        return new(rawUri, UriKind.Absolute);
    }

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance state, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(state, definition, contextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnSubTaskFaultAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false));
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (function == null) throw new InvalidOperationException("The executor must be initialized before execution");
        JsonNode? input;
        if (Task.Definition.With != null)
        {
            var evaluated = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.With, Task.Instance.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
            input = evaluated ?? new JsonObject();
        }
        else
        {
            input = new JsonObject();
        }
        var taskInstance = await Task.Workflow.CreateTaskAsync(function, JsonPointer.Empty, input, Task, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTaskExecutorAsync(taskInstance, function, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
        await SetResultAsync(executor.Task.Instance.Output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubTaskFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(executor);
        var error = executor.Task.Instance.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

}
