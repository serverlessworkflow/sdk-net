using System.Net;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services.Executors;

public class HttpCallTaskExecutorTests
    : TaskExecutorTestsBase
{

    [Fact]
    public async Task Execute_Should_Send_Http_Request_And_Set_Result()
    {
        // arrange
        var with = new JsonObject { ["method"] = "GET", ["endpoint"] = "https://api.example.com/data" };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{\"id\":1}", System.Text.Encoding.UTF8, "application/json")
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
        handler.RequestReceived.Should().NotBeNull();
        handler.RequestReceived!.Method.Should().Be(HttpMethod.Get);
    }

    [Fact]
    public async Task Execute_Should_Set_Error_On_Non_Success_StatusCode()
    {
        // arrange
        var with = new JsonObject { ["method"] = "GET", ["endpoint"] = "https://api.example.com/data" };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("Server Error")
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetErrorAsync(
                It.Is<IRuntimeError>(e => e.Status == 500),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Execute_Should_Send_Post_With_Json_Body()
    {
        // arrange
        var with = new JsonObject
        {
            ["method"] = "POST",
            ["endpoint"] = "https://api.example.com/data",
            ["headers"] = new JsonObject { ["Content-Type"] = "text/plain" },
            ["body"] = "hello world"
        };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent("{\"created\":true}", System.Text.Encoding.UTF8, "application/json")
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
    }

    [Fact]
    public async Task Execute_Should_Use_Authentication_When_Configured()
    {
        // arrange
        var with = new JsonObject
        {
            ["method"] = "GET",
            ["endpoint"] = new JsonObject
            {
                ["uri"] = "https://api.example.com/secure",
                ["authentication"] = new JsonObject { ["use"] = "my-auth" }
            }
        };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json")
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var expectedScheme = "Bearer";
        var expectedToken = "test-token";
        var authResult = new Mock<IAuthenticationResult>();
        authResult.Setup(r => r.Scheme).Returns(expectedScheme);
        authResult.Setup(r => r.Value).Returns(expectedToken);
        var authHandler = new Mock<IAuthenticationHandler>();
        authHandler.Setup(h => h.HandleAsync(It.IsAny<AuthenticationPolicyDefinition>(), It.IsAny<WorkflowDefinition?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(authResult.Object);
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        authHandler.Verify(h => h.HandleAsync(It.IsAny<AuthenticationPolicyDefinition>(), It.IsAny<WorkflowDefinition?>(), It.IsAny<CancellationToken>()), Times.Once);
        handler.RequestReceived!.Headers.Authorization.Should().NotBeNull();
        handler.RequestReceived!.Headers.Authorization!.Scheme.Should().Be(expectedScheme);
        handler.RequestReceived!.Headers.Authorization!.Parameter.Should().Be(expectedToken);
    }

    [Fact]
    public async Task Execute_Should_Return_Full_Response_When_Output_Is_Response()
    {
        // arrange
        var with = new JsonObject
        {
            ["method"] = "GET",
            ["endpoint"] = "https://api.example.com/data",
            ["output"] = HttpOutputFormat.Response
        };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Running);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("{\"data\":\"value\"}", System.Text.Encoding.UTF8, "application/json")
        });
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.InitializeAsync(TestContext.Current.CancellationToken);
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetResultAsync(It.IsAny<JsonNode?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce);
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.SetErrorAsync(It.IsAny<IRuntimeError>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_Should_Skip_When_Already_Completed()
    {
        // arrange
        var with = new JsonObject { ["method"] = "GET", ["endpoint"] = "https://example.com" };
        var definition = new CallTaskDefinition { Call = Function.Http, With = with };
        var taskContext = CreateTaskExecutionContext(definition);
        Mock.Get(taskContext.Object.Instance.State).Setup(s => s.Status).Returns(TaskInstanceStatus.Completed);
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var authHandler = new Mock<IAuthenticationHandler>();
        var executor = CreateExecutor(taskContext, httpClientFactory.Object, authHandler.Object);

        // act
        await executor.ExecuteAsync(TestContext.Current.CancellationToken);

        // assert
        Mock.Get(taskContext.Object.Instance).Verify(
            i => i.StartAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }

    static HttpCallTaskExecutor CreateExecutor(Mock<ITaskExecutionContext<CallTaskDefinition>> taskContext, IHttpClientFactory httpClientFactory, IAuthenticationHandler authHandler) => new(
        CreateServiceProvider().Object,
        Mock.Of<ILogger<HttpCallTaskExecutor>>(),
        CreateExecutionContextFactory().Object,
        CreateExecutorFactory().Object,
        CreateSchemaHandlerProvider().Object,
        httpClientFactory,
        authHandler,
        taskContext.Object);

    class MockHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        public HttpRequestMessage? RequestReceived { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestReceived = request;
            return Task.FromResult(response);
        }
    }

}
