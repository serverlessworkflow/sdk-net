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

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="EmitTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="ICloudEvent"/>s</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class EmitTaskExecutor(IServiceProvider serviceProvider, ILogger<EmitTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ICloudEventBus cloudEventBus, ITaskExecutionContext<EmitTaskDefinition> task)
    : TaskExecutor<EmitTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var attributes = Task.Definition.Emit.Event.With.DeepClone().AsObject() ?? new JsonObject();
        if (!attributes.ContainsKey(CloudEventAttributes.Id)) attributes[CloudEventAttributes.Id] = Guid.NewGuid().ToString();
        if (!attributes.ContainsKey(CloudEventAttributes.SpecVersion)) attributes[CloudEventAttributes.SpecVersion] = CloudEvent.DefaultVersion;
        if (!attributes.ContainsKey(CloudEventAttributes.Time)) attributes[CloudEventAttributes.Time] = DateTimeOffset.Now.ToString("o");
        var result = await Task.Workflow.Expressions.EvaluateAsync(attributes, Task.Instance.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        var cloudEvent = JsonSerializer.Deserialize(result, Serialization.Json.JsonSerializationContext.Default.CloudEvent);
        await cloudEventBus.PublishAsync(cloudEvent!, cancellationToken).ConfigureAwait(false);
        await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
