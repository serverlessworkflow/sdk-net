using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;
using SdkTaskStatus = ServerlessWorkflow.Sdk.Runtime.TaskStatus;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class TaskInstanceTests
{

    static TaskInstance CreateInstance() => new()
    {
        WorkflowId = "wf-1",
        Reference = JsonPointer.Parse("/test"),
        Input = new JsonObject { ["key"] = "value" }
    };

    static Error CreateError() => Error.Runtime(new Uri("https://example.com/errors/test"), "Something went wrong");

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = CreateInstance();
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.TaskInstance);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.TaskInstance);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(toSerialize.Id);
        deserialized.WorkflowId.Should().Be(toSerialize.WorkflowId);
        deserialized.Reference.Should().Be(toSerialize.Reference);
    }

    [Fact]
    public async Task StartAsync_Should_Set_Status_And_StartedAt_And_Add_Run()
    {
        //arrange
        var instance = CreateInstance();
        //act
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Running);
        instance.StartedAt.Should().NotBeNull();
        instance.Runs.Should().NotBeNull();
        instance.Runs!.Count.Should().Be(1);
    }

    [Fact]
    public async Task SuspendAsync_Should_Set_Status_And_End_Current_Run()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //act
        await instance.SuspendAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Suspended);
        instance.Runs.Should().NotBeNull();
        var run = instance.Runs!.Last();
        run.EndedAt.Should().NotBeNull();
        run.Outcome.Should().Be(SdkTaskStatus.Suspended);
    }

    [Fact]
    public async Task ResumeAsync_Should_Set_Status_And_Add_New_Run()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        await instance.SuspendAsync(TestContext.Current.CancellationToken);
        //act
        await instance.ResumeAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Running);
        instance.Runs.Should().NotBeNull();
        instance.Runs!.Count.Should().Be(2);
    }

    [Fact]
    public async Task SetOutputAsync_Should_Set_Completed_Status_And_Output_And_Next()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        var output = new JsonObject { ["result"] = "done" };
        var next = "/next-task";
        //act
        await instance.SetOutputAsync(output, next, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Completed);
        instance.Output.Should().NotBeNull();
        instance.Next.Should().Be(next);
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task SetErrorAsync_Should_Set_Faulted_Status_And_EndedAt()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        var error = CreateError();
        //act
        await instance.SetErrorAsync(error, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Faulted);
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task SkipAsync_Should_Set_Skipped_Status_And_Output_And_Next()
    {
        //arrange
        var instance = CreateInstance();
        var output = new JsonObject { ["skipped"] = true };
        var next = "/after-skip";
        //act
        await instance.SkipAsync(output, next, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Skipped);
        instance.Output.Should().NotBeNull();
        instance.Next.Should().Be(next);
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task CancelAsync_Should_Set_Cancelled_Status_And_EndedAt()
    {
        //arrange
        var instance = CreateInstance();
        await instance.StartAsync(TestContext.Current.CancellationToken);
        //act
        await instance.CancelAsync(TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Cancelled);
        instance.EndedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task RetryAsync_Should_Add_RetryAttempt_And_Set_Running_Status()
    {
        //arrange
        var instance = CreateInstance();
        var error = CreateError();
        //act
        await instance.RetryAsync(error, TestContext.Current.CancellationToken);
        //assert
        instance.Status.Should().Be(SdkTaskStatus.Running);
        instance.Retries.Should().NotBeNull();
        instance.Retries!.Count.Should().Be(1);
        instance.Retries!.First().Number.Should().Be(1u);
        instance.Retries!.First().Cause.Should().Be(error);
        instance.Runs.Should().NotBeNull();
        instance.Runs!.Count.Should().Be(1);
        instance.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task RetryAsync_Multiple_Times_Should_Increment_Attempt_Number()
    {
        //arrange
        var instance = CreateInstance();
        var error1 = CreateError();
        var error2 = CreateError();
        //act
        await instance.RetryAsync(error1, TestContext.Current.CancellationToken);
        await instance.RetryAsync(error2, TestContext.Current.CancellationToken);
        //assert
        instance.Retries.Should().NotBeNull();
        instance.Retries!.Count.Should().Be(2);
        instance.Retries!.Last().Number.Should().Be(2u);
        instance.Runs!.Count.Should().Be(2);
    }

    [Fact]
    public void Default_Values_Should_Be_Set()
    {
        //arrange & act
        var instance = CreateInstance();
        //assert
        instance.Id.Should().NotBeNullOrWhiteSpace();
        instance.WorkflowId.Should().Be("wf-1");
        instance.Status.Should().BeNull();
        instance.StartedAt.Should().BeNull();
        instance.EndedAt.Should().BeNull();
        instance.Runs.Should().BeNull();
        instance.Retries.Should().BeNull();
    }

}
