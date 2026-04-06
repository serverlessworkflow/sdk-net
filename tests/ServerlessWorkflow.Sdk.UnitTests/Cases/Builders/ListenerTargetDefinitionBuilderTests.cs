namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ListenerTargetDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Target_With_All_Events()
    {
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.All().Event(f => f.With("type", eventType));
        var target = builder.Build();
        target.All.Should().NotBeNull();
        target.Any.Should().BeNull();
        target.One.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Target_With_Any_Events()
    {
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.Any().Event(f => f.With("type", eventType));
        var target = builder.Build();
        target.Any.Should().NotBeNull();
        target.All.Should().BeNull();
        target.One.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Target_With_One_Event()
    {
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.One().With("type", eventType);
        var target = builder.Build();
        target.One.Should().NotBeNull();
        target.All.Should().BeNull();
        target.Any.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Strategy()
    {
        var act = () => new ListenerTargetDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
