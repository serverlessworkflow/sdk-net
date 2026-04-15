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

public class RaiseTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Raise_Inline_Error()
    {
        // Arrange
        var errorDef = new ErrorDefinition
        {
            Type = "https://example.com/errors/not-found",
            Title = "Not Found",
            Status = "404",
            Detail = "Resource not found"
        };
        var definition = new RaiseTaskDefinition
        {
            Raise = new RaiseErrorDefinition { Error = new OneOf<ErrorDefinition, string>(errorDef) }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var executor = new RaiseTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RaiseTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            c => c.SetErrorAsync(
                It.Is<Error>(e =>
                    e.Status == 404 &&
                    e.Title == "Not Found"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Raise_Referenced_Error()
    {
        // Arrange
        var errorDef = new ErrorDefinition
        {
            Type = "https://example.com/errors/timeout",
            Title = "Timeout",
            Status = "408"
        };
        var definition = new RaiseTaskDefinition
        {
            Raise = new RaiseErrorDefinition { Error = new OneOf<ErrorDefinition, string>("timeoutError") }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        // Set up the workflow definition with the referenced error
        var errors = new EquatableDictionary<string, ErrorDefinition> { ["timeoutError"] = errorDef };
        var workflowDef = new WorkflowDefinition
        {
            Document = new WorkflowDefinitionMetadata { Dsl = "1.0.0", Name = "test", Namespace = "test", Version = "1.0.0" },
            Do = [],
            Use = new ComponentDefinitionCollection { Errors = errors }
        };
        Mock.Get(taskContext.Object.Workflow).Setup(w => w.Definition).Returns(workflowDef);

        var executor = new RaiseTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RaiseTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            c => c.SetErrorAsync(
                It.Is<Error>(e =>
                    e.Status == 408 &&
                    e.Title == "Timeout"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Fault_When_Referenced_Error_Not_Found()
    {
        // Arrange
        var definition = new RaiseTaskDefinition
        {
            Raise = new RaiseErrorDefinition { Error = new OneOf<ErrorDefinition, string>("nonExistentError") }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var executor = new RaiseTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RaiseTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set a runtime error because the reference was not found
        taskContext.Verify(
            c => c.SetErrorAsync(
                It.Is<Error>(e => e.Type == ErrorType.Runtime),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Set_Instance_From_Task_Reference()
    {
        // Arrange
        var errorDef = new ErrorDefinition
        {
            Type = "https://example.com/errors/bad-request",
            Title = "Bad Request",
            Status = "400"
        };
        var definition = new RaiseTaskDefinition
        {
            Raise = new RaiseErrorDefinition { Error = new OneOf<ErrorDefinition, string>(errorDef) }
        };
        var reference = JsonPointer.Parse("/do/0/myTask");
        var taskContext = CreateTaskExecutionContext(definition, reference: reference);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var executor = new RaiseTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<RaiseTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            c => c.SetErrorAsync(
                It.Is<Error>(e => e.Instance != null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

}
