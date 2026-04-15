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

using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.IO;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class AsyncApiCallTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Set_Error_When_Document_Fetch_Fails()
    {
        // arrange
        var with = new JsonObject
        {
            ["document"] = new JsonObject { ["endpoint"] = "https://api.example.com/asyncapi.json" },
            ["operation"] = "sendMessage"
        };
        var definition = new CallTaskDefinition { Call = Function.AsyncApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Not Found") });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var asyncApiDocReader = new Mock<IAsyncApiDocumentReader>();
        var asyncApiClientFactory = new Mock<IAsyncApiClientFactory>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object, asyncApiDocReader.Object, asyncApiClientFactory.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var with = new JsonObject
        {
            ["document"] = new JsonObject { ["endpoint"] = "https://api.example.com/asyncapi.json" },
            ["operation"] = "sendMessage"
        };
        var definition = new CallTaskDefinition { Call = Function.AsyncApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var asyncApiDocReader = new Mock<IAsyncApiDocumentReader>();
        var asyncApiClientFactory = new Mock<IAsyncApiClientFactory>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object, asyncApiDocReader.Object, asyncApiClientFactory.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_Should_Set_Validation_Error_When_With_Is_Invalid()
    {
        // arrange
        var with = new JsonObject { ["invalid"] = "data" };
        var definition = new CallTaskDefinition { Call = Function.AsyncApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance).Setup(s => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var asyncApiDocReader = new Mock<IAsyncApiDocumentReader>();
        var asyncApiClientFactory = new Mock<IAsyncApiClientFactory>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object, asyncApiDocReader.Object, asyncApiClientFactory.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        taskContext.Verify(
            i => i.SetErrorAsync(It.IsAny<Error>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    static AsyncApiCallTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<CallTaskDefinition>> taskContext, IHttpClientFactory httpClientFactory, IAuthenticationHandler authHandler, IAsyncApiDocumentReader asyncApiDocReader, IAsyncApiClientFactory asyncApiClientFactory) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<AsyncApiCallTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        httpClientFactory,
        authHandler,
        asyncApiDocReader,
        asyncApiClientFactory,
        taskContext.Object);

    class MockHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(response);
    }

}
