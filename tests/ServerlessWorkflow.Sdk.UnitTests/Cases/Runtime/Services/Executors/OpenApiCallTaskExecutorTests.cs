using System.Net;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class OpenApiCallTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Fetch_Document_And_Call_Operation()
    {
        // arrange
        var openApiDoc = """
        {
            "openapi": "3.0.0",
            "info": { "title": "Test", "version": "1.0" },
            "servers": [{ "url": "https://api.example.com" }],
            "paths": {
                "/pets": {
                    "get": {
                        "operationId": "listPets",
                        "responses": { "200": { "description": "OK" } }
                    }
                }
            }
        }
        """;
        var with = new JsonObject
        {
            ["document"] = new JsonObject { ["endpoint"] = "https://api.example.com/openapi.json" },
            ["operationId"] = "listPets"
        };
        var definition = new CallTaskDefinition { Call = Function.OpenApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var requestIndex = 0;
        var handler = new MockHttpMessageHandler(request =>
        {
            requestIndex++;
            if (requestIndex == 1) return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(openApiDoc, System.Text.Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("[{\"name\":\"Fido\"}]", System.Text.Encoding.UTF8, "application/json") };
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(() => new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Set_Error_On_Non_Success_Api_Response()
    {
        // arrange
        var openApiDoc = """
        {
            "openapi": "3.0.0",
            "info": { "title": "Test", "version": "1.0" },
            "servers": [{ "url": "https://api.example.com" }],
            "paths": {
                "/pets": {
                    "get": {
                        "operationId": "listPets",
                        "responses": { "200": { "description": "OK" } }
                    }
                }
            }
        }
        """;
        var with = new JsonObject
        {
            ["document"] = new JsonObject { ["endpoint"] = "https://api.example.com/openapi.json" },
            ["operationId"] = "listPets"
        };
        var definition = new CallTaskDefinition { Call = Function.OpenApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Running);
        var requestIndex = 0;
        var handler = new MockHttpMessageHandler(request =>
        {
            requestIndex++;
            if (requestIndex == 1) return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(openApiDoc, System.Text.Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent("Error") };
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(() => new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.SetErrorAsync(
                It.Is<Error>(e => e.Status == 500),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var with = new JsonObject
        {
            ["document"] = new JsonObject { ["endpoint"] = "https://api.example.com/openapi.json" },
            ["operationId"] = "listPets"
        };
        var definition = new CallTaskDefinition { Call = Function.OpenApi, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.State.State).Setup((T s) => s.Status).Returns(Sdk.Runtime.TaskStatus.Completed);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.State).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static OpenApiCallTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<CallTaskDefinition>> taskContext, IHttpClientFactory httpClientFactory, IAuthenticationHandler authHandler) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<OpenApiCallTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        httpClientFactory,
        authHandler,
        taskContext.Object);

    class MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handler) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(handler(request));
    }

}
