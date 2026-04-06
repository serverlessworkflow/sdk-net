namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class GenericTaskDefinitionBuilderTests
{

    [Fact]
    public void Call_Should_Return_CallTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Call("myFunc").With("arg", JsonValue.Create("val"));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<CallTaskDefinition>();
        ((CallTaskDefinition)task).Call.Should().Be("myFunc");
    }

    [Fact]
    public void Set_With_Name_Value_Should_Return_SetTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Set("greeting", "hello");
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<SetTaskDefinition>();
    }

    [Fact]
    public void Set_With_JsonObject_Should_Return_SetTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        var key = "k";
        var value = "v";
        builder.Set(new JsonObject { [key] = value });
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<SetTaskDefinition>();
    }

    [Fact]
    public void Wait_Should_Return_WaitTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Wait(Duration.FromSeconds(5));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<WaitTaskDefinition>();
    }

    [Fact]
    public void Emit_With_EventDefinition_Should_Return_EmitTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        var e = new EventDefinition { With = new JsonObject { ["type"] = "com.test" } };
        builder.Emit(e);
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<EmitTaskDefinition>();
    }

    [Fact]
    public void Emit_With_Setup_Should_Return_EmitTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Emit(e => e.With("type", JsonValue.Create("com.test")));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<EmitTaskDefinition>();
    }

    [Fact]
    public void Raise_With_ErrorDefinition_Should_Return_RaiseTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        var error = new ErrorDefinition { Type = "https://err.com/t", Title = "T", Status = "500" };
        builder.Raise(error);
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<RaiseTaskDefinition>();
    }

    [Fact]
    public void Raise_With_Setup_Should_Return_RaiseTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Raise(e => e.WithType("https://err.com/t").WithTitle("T").WithStatus("500"));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<RaiseTaskDefinition>();
    }

    [Fact]
    public void For_Should_Return_ForTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.For().Each("item").In("${ .items }").Do(tasks => tasks.Do("process", t => t.Set("k", "v")));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<ForTaskDefinition>();
    }

    [Fact]
    public void Fork_Should_Return_ForkTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Fork().Branch(tasks =>
        {
            tasks.Do("b1", t => t.Set("a", "1"));
            tasks.Do("b2", t => t.Set("b", "2"));
        });
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<ForkTaskDefinition>();
    }

    [Fact]
    public void Switch_Should_Return_SwitchTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Switch().Case("default", c => c.Then(FlowDirective.End));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<SwitchTaskDefinition>();
    }

    [Fact]
    public void Try_Should_Return_TryTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Try()
            .Do(tasks => tasks.Do("risky", t => t.Set("k", "v")))
            .Catch(c => { });
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<TryTaskDefinition>();
    }

    [Fact]
    public void Run_Should_Return_RunTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Run().Shell().WithCommand("echo test");
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<RunTaskDefinition>();
    }

    [Fact]
    public void Listen_Should_Return_ListenTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Listen().To(l => l.One().With("type", JsonValue.Create("com.test")));
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<ListenTaskDefinition>();
    }

    [Fact]
    public void Do_Should_Return_DoTaskDefinition()
    {
        // arrange
        var builder = new GenericTaskDefinitionBuilder();
        builder.Do(tasks =>
        {
            tasks.Do("s1", t => t.Set("a", "1"));
            tasks.Do("s2", t => t.Set("b", "2"));
        });
        // act
        var task = builder.Build();
        // assert
        task.Should().BeOfType<DoTaskDefinition>();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Task_Configured()
    {
        // act
        var act = () => new GenericTaskDefinitionBuilder().Build();
        // assert
        act.Should().Throw<NullReferenceException>();
    }

}
