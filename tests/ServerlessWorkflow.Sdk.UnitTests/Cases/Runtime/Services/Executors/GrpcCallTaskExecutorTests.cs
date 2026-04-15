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

public class GrpcCallTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Set_Validation_Error_When_Proto_Cannot_Be_Loaded()
    {
        // arrange
        var with = new JsonObject
        {
            ["proto"] = new JsonObject { ["endpoint"] = "https://example.com/service.proto" },
            ["service"] = new JsonObject { ["name"] = "Greeter", ["host"] = "localhost", ["port"] = 50051 },
            ["method"] = "SayHello"
        };
        var definition = new CallTaskDefinition { Call = Function.Grpc, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var externalResourceReader = new Mock<IExternalResourceReader>();
        externalResourceReader
            .Setup(r => r.ReadAsync(It.IsAny<ExternalResourceDefinition>(), It.IsAny<WorkflowDefinition?>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Failed to fetch proto file"));
        var executor = CreateExecutor(taskContext, externalResourceReader.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.SetErrorAsync(
                It.Is<Error>(e => e.Type == ErrorType.Validation),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Set_Validation_Error_When_With_Is_Invalid()
    {
        // arrange
        var with = new JsonObject { ["invalid"] = "data" };
        var definition = new CallTaskDefinition { Call = Function.Grpc, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var externalResourceReader = new Mock<IExternalResourceReader>();
        var executor = CreateExecutor(taskContext, externalResourceReader.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.SetErrorAsync(
                It.Is<Error>(e => e.Type == ErrorType.Validation),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var with = new JsonObject
        {
            ["proto"] = new JsonObject { ["endpoint"] = "https://example.com/service.proto" },
            ["service"] = new JsonObject { ["name"] = "Greeter", ["host"] = "localhost" },
            ["method"] = "SayHello"
        };
        var definition = new CallTaskDefinition { Call = Function.Grpc, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        var externalResourceReader = new Mock<IExternalResourceReader>();
        var executor = CreateExecutor(taskContext, externalResourceReader.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static GrpcCallTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<CallTaskDefinition>> taskContext, IExternalResourceReader externalResourceReader) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<GrpcCallTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        externalResourceReader,
        taskContext.Object);

}
