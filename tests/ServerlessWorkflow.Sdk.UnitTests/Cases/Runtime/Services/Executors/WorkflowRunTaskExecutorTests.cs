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

using ServerlessWorkflow.Sdk.Runtime.Configuration;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class WorkflowRunTaskExecutorTests
    : TaskExecutorTestsBase
{

    static RunTaskDefinition CreateDefinition(bool? @await = null, JsonObject? input = null) => new()
    {
        Run = new ProcessTypeDefinition
        {
            Workflow = new WorkflowProcessDefinition
            {
                Namespace = "test",
                Name = "sub-workflow",
                Version = "1.0.0",
                Input = input
            },
            Await = @await
        }
    };

    static WorkflowDefinition CreateSubflowDefinition() => new()
    {
        Document = new WorkflowDefinitionMetadata
        {
            Dsl = "1.0.0",
            Namespace = "test",
            Name = "sub-workflow",
            Version = "1.0.0"
        },
        Do = []
    };

    static (Mock<IWorkflowProcess> process, Subject<IWorkflowLifeCycleEvent> events) CreateProcessMock()
    {
        var events = new Subject<IWorkflowLifeCycleEvent>();
        var process = new Mock<IWorkflowProcess>();
        process.Setup(p => p.Subscribe(It.IsAny<IObserver<IWorkflowLifeCycleEvent>>()))
            .Returns((IObserver<IWorkflowLifeCycleEvent> o) => events.Subscribe(o));
        process.Setup(p => p.CancelAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        return (process, events);
    }

    static WorkflowRunTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<RunTaskDefinition>> taskContext, Mock<IWorkflowDefinitionStore> definitions) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<WorkflowRunTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        taskContext.Object,
        definitions.Object);

    [Fact]
    public async Task Execute_Should_Resolve_Definition_Run_Subflow_And_Set_Result_With_Output()
    {
        // Arrange
        var definition = CreateDefinition();
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var subflowDefinition = CreateSubflowDefinition();
        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync("test", "sub-workflow", "1.0.0", It.IsAny<CancellationToken>()))
            .ReturnsAsync(subflowDefinition);

        var expectedOutput = new JsonObject { ["result"] = "ok" };
        var (process, events) = CreateProcessMock();
        process.Setup(p => p.WaitAsync(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                events.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Completed, expectedOutput));
                return Task.CompletedTask;
            });

        var runtimeMock = Mock.Get(taskContext.Object.Workflow.Runtime);
        runtimeMock.Setup(r => r.RunAsync(subflowDefinition, It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(process.Object);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        definitions.Verify(d => d.GetAsync("test", "sub-workflow", "1.0.0", It.IsAny<CancellationToken>()), Times.Once);
        runtimeMock.Verify(r => r.RunAsync(subflowDefinition, It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()), Times.Once);
        taskContext.Verify(c => c.SetResultAsync(It.Is<JsonNode?>(n => n == expectedOutput), It.IsAny<string?>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        taskContext.Verify(c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Execute_Should_Evaluate_Input_Expression_Before_Running_Subflow()
    {
        // Arrange
        var inputExpression = new JsonObject { ["mapped"] = "${ .value }" };
        var definition = CreateDefinition(input: inputExpression);
        var parentInput = new JsonObject { ["value"] = 42 };
        var taskContext = CreateTaskExecutionContext(definition, parentInput);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var evaluatedInput = new JsonObject { ["mapped"] = 42 };
        Mock.Get(taskContext.Object.Workflow.Expressions)
            .Setup(e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(evaluatedInput);

        var subflowDefinition = CreateSubflowDefinition();
        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(subflowDefinition);

        JsonObject? capturedInput = null;
        var (process, events) = CreateProcessMock();
        process.Setup(p => p.WaitAsync(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                events.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Completed, new JsonObject()));
                return Task.CompletedTask;
            });

        Mock.Get(taskContext.Object.Workflow.Runtime)
            .Setup(r => r.RunAsync(It.IsAny<WorkflowDefinition>(), It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()))
            .Callback((WorkflowDefinition _, JsonObject? i, WorkflowExecutionsOptions? _, CancellationToken _) => capturedInput = i)
            .ReturnsAsync(process.Object);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        Mock.Get(taskContext.Object.Workflow.Expressions).Verify(
            e => e.EvaluateAsync(It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
        capturedInput.Should().NotBeNull();
    }

    [Fact]
    public async Task Execute_Should_Return_Immediately_When_Await_Is_False()
    {
        // Arrange
        var definition = CreateDefinition(@await: false);
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateSubflowDefinition());

        var (process, _) = CreateProcessMock();
        Mock.Get(taskContext.Object.Workflow.Runtime)
            .Setup(r => r.RunAsync(It.IsAny<WorkflowDefinition>(), It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(process.Object);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        process.Verify(p => p.WaitAsync(It.IsAny<CancellationToken>()), Times.Never);
        taskContext.Verify(c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Set_Error_When_Subflow_Faults()
    {
        // Arrange
        var definition = CreateDefinition();
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateSubflowDefinition());

        var subflowError = new Error
        {
            Type = ErrorType.Runtime,
            Status = ErrorStatus.Runtime,
            Title = ErrorTitle.Runtime,
            Detail = "Boom"
        };
        var (process, events) = CreateProcessMock();
        process.Setup(p => p.WaitAsync(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                events.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Faulted, subflowError));
                return Task.FromException(new RuntimeErrorException(subflowError));
            });

        Mock.Get(taskContext.Object.Workflow.Runtime)
            .Setup(r => r.RunAsync(It.IsAny<WorkflowDefinition>(), It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(process.Object);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(c => c.SetErrorAsync(It.Is<Error>(e => e.Detail == "Boom"), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        taskContext.Verify(c => c.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Execute_Should_Set_Error_When_Definition_Not_Found()
    {
        // Arrange
        var definition = CreateDefinition();
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((WorkflowDefinition?)null);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        Mock.Get(taskContext.Object.Workflow.Runtime).Verify(
            r => r.RunAsync(It.IsAny<WorkflowDefinition>(), It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Cancel_Should_Propagate_To_Running_Subflow()
    {
        // Arrange
        var definition = CreateDefinition();
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(i => i.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var definitions = new Mock<IWorkflowDefinitionStore>();
        definitions.Setup(d => d.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateSubflowDefinition());

        var waitTcs = new TaskCompletionSource();
        var (process, _) = CreateProcessMock();
        process.Setup(p => p.WaitAsync(It.IsAny<CancellationToken>())).Returns(waitTcs.Task);
        process.Setup(p => p.CancelAsync(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                waitTcs.TrySetCanceled();
                return Task.CompletedTask;
            });

        Mock.Get(taskContext.Object.Workflow.Runtime)
            .Setup(r => r.RunAsync(It.IsAny<WorkflowDefinition>(), It.IsAny<JsonObject?>(), It.IsAny<WorkflowExecutionsOptions?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(process.Object);

        var executor = CreateExecutor(taskContext, definitions);

        // Act
        var executeTask = executor.ExecuteAsync(TestContext.Current.CancellationToken);
        await Task.Yield();
        await executor.CancelAsync(TestContext.Current.CancellationToken);
        await executeTask;

        // Assert
        process.Verify(p => p.CancelAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

}
