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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public abstract class TaskExecutorTestsBase
{

    protected static Mock<ITaskExecutionContext<TDefinition>> CreateTaskExecutionContext<TDefinition>(
        TDefinition definition, JsonNode? input = null, JsonObject? contextData = null,
        JsonObject? arguments = null, string? taskStatus = null, string? taskName = null, JsonPointer? reference = null)
        where TDefinition : TaskDefinition
    {
        input ??= new JsonObject();
        contextData ??= [];
        arguments ??= [];
        var effectiveReference = reference ?? JsonPointer.Parse("/test");

        var taskInstance = new Mock<ITaskInstance> { CallBase = true };
        taskInstance.Setup(i => i.Id).Returns("task-1");
        taskInstance.Setup(i => i.WorkflowId).Returns("workflow-1");
        taskInstance.Setup(i => i.Status).Returns(taskStatus);
        taskInstance.Setup(i => i.Name).Returns(taskName);
        taskInstance.Setup(i => i.Reference).Returns(effectiveReference);
        taskInstance.Setup(i => i.IsExtension).Returns(false);
        taskInstance.Setup(i => i.Input).Returns(input);
        taskInstance.Setup(i => i.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SetOutputAsync(It.IsAny<JsonNode?>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SuspendAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskInstance.Setup(i => i.SkipAsync(It.IsAny<JsonNode?>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var expressionEvaluator = new Mock<IRuntimeExpressionEvaluator>();
        expressionEvaluator.Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((JsonNode?)null);

        var runtimeDescriptor = new RuntimeDescriptor() { Name = "test-runtime", Version = "1.0.0" };
        var runtime = new Mock<IWorkflowRuntime>();
        runtime.Setup(r => r.Descriptor).Returns(runtimeDescriptor);

        var workflowDefinition = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata
            {
                Dsl = "1.0.0",
                Name = "test-workflow",
                Namespace = "test",
                Version = "1.0.0"
            },
            Do = []
        };

        var workflowInstance = new Mock<IWorkflowInstance>();
        workflowInstance.Setup(i => i.Id).Returns("workflow-1");
        workflowInstance.Setup(i => i.ContextData).Returns(contextData);

        var workflow = new Mock<IWorkflowExecutionContext>();
        workflow.Setup(w => w.Definition).Returns(workflowDefinition);
        workflow.Setup(w => w.Instance).Returns(workflowInstance.Object);
        workflow.Setup(w => w.Expressions).Returns(expressionEvaluator.Object);
        workflow.Setup(w => w.Runtime).Returns(runtime.Object);
        workflow.Setup(w => w.GetExpressionEvaluationArguments()).Returns(new JsonObject());
        workflow.Setup(w => w.CreateTaskAsync(
            It.IsAny<TaskDefinition>(), It.IsAny<JsonPointer>(), It.IsAny<JsonNode>(),
            It.IsAny<ITaskExecutionContext?>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns((TaskDefinition def, JsonPointer path, JsonNode inp, ITaskExecutionContext? parent, bool isExt, CancellationToken ct) =>
            {
                var subInstance = new Mock<ITaskInstance> { CallBase = true };
                subInstance.Setup(i => i.Id).Returns(Guid.NewGuid().ToString());
                subInstance.Setup(i => i.WorkflowId).Returns("workflow-1");
                subInstance.Setup(i => i.Reference).Returns(path);
                subInstance.Setup(i => i.Name).Returns(path.ToString().Split('/').Last());
                subInstance.Setup(i => i.IsExtension).Returns(isExt);
                subInstance.Setup(i => i.Input).Returns(inp);
                subInstance.Setup(i => i.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SetOutputAsync(It.IsAny<JsonNode?>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SuspendAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                subInstance.Setup(i => i.SkipAsync(It.IsAny<JsonNode?>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                return Task.FromResult(subInstance.Object);
            });

        var taskContext = new Mock<ITaskExecutionContext<TDefinition>>();
        taskContext.Setup(c => c.Workflow).Returns(workflow.Object);
        taskContext.Setup(c => c.Definition).Returns(definition);
        taskContext.Setup(c => c.Instance).Returns(taskInstance.Object);
        taskContext.Setup(c => c.Arguments).Returns(() => arguments.DeepClone().AsObject()!);
        taskContext.Setup(c => c.StartAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.SetContextDataAsync(It.IsAny<JsonObject>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.SkipAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.SuspendAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        taskContext.Setup(c => c.GetSubTasksAsync(It.IsAny<CancellationToken>())).Returns(AsyncEnumerableEmpty<ITaskInstance>());

        return taskContext;
    }

    protected static Mock<IServiceProvider> CreateServiceProvider() => new();

    protected static Mock<ITaskExecutionContextFactory> CreateExecutionContextFactory() => new();

    protected static Mock<ITaskExecutorFactory> CreateExecutorFactory() => new();

    protected static Mock<ISchemaHandlerProvider> CreateSchemaHandlerProvider() => new();

    protected static async IAsyncEnumerable<T> AsyncEnumerableEmpty<T>()
    {
        await Task.CompletedTask;
        yield break;
    }

    protected static async IAsyncEnumerable<T> AsyncEnumerableOf<T>(params T[] items)
    {
        foreach (var item in items)
        {
            yield return item;
            await Task.CompletedTask;
        }
    }

}
