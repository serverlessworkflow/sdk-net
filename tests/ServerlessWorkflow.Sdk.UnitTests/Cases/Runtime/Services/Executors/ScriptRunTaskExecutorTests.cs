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

public class ScriptRunTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Script_Should_Skip_When_Already_Completed()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Script = new ScriptProcessDefinition { Language = "js", Code = "console.log('hello');" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);

        var externalResourceReader = new Mock<IExternalResourceReader>();
        var scriptExecutorProvider = new Mock<IScriptExecutorProvider>();
        var executor = new ScriptRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ScriptRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            externalResourceReader.Object,
            scriptExecutorProvider.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert
        taskContext.Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_Script_Should_Set_Error_When_Code_And_Source_Are_Null()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Script = new ScriptProcessDefinition { Language = "js" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var scriptExecutor = new Mock<IScriptExecutor>();
        var scriptExecutorProvider = new Mock<IScriptExecutorProvider>();
        scriptExecutorProvider.Setup(p => p.GetExecutor("js")).Returns(scriptExecutor.Object);

        var externalResourceReader = new Mock<IExternalResourceReader>();
        var executor = new ScriptRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ScriptRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            externalResourceReader.Object,
            scriptExecutorProvider.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set error due to exception
        taskContext.Verify(
            c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Script_Should_Set_Error_When_Language_Not_Supported()
    {
        // Arrange
        var definition = new RunTaskDefinition
        {
            Run = new ProcessTypeDefinition { Script = new ScriptProcessDefinition { Language = "unsupported-lang", Code = "some code" } }
        };
        var taskContext = CreateTaskExecutionContext(definition);

        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);

        var expressionMock = Mock.Get(taskContext.Object.Workflow.Expressions);
        expressionMock.Setup(e => e.EvaluateAsync(
            It.IsAny<string>(), It.IsAny<JsonNode>(), It.IsAny<JsonObject?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string expr, JsonNode inp, JsonObject? args, CancellationToken ct) => inp);

        var scriptExecutorProvider = new Mock<IScriptExecutorProvider>();
        scriptExecutorProvider.Setup(p => p.GetExecutor("unsupported-lang")).Returns((IScriptExecutor?)null);

        var externalResourceReader = new Mock<IExternalResourceReader>();
        var executor = new ScriptRunTaskExecutor(
            CreateServiceProvider().Object,
            Mock.Of<ILogger<ScriptRunTaskExecutor>>(),
            CreateExecutionContextFactory().Object,
            CreateExecutorFactory().Object,
            CreateSchemaHandlerProvider().Object,
            externalResourceReader.Object,
            scriptExecutorProvider.Object,
            taskContext.Object);

        // Act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // Assert - should set error due to unsupported language
        taskContext.Verify(
            c => c.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

}
