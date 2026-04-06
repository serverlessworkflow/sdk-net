namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ListenerTargetDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Target_With_All_Events()
    {
        //arrange
        var typeKey = "type";
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.All().Event(f => f.With(typeKey, eventType));

        //act
        var target = builder.Build();

        //assert
        target.All.Should().NotBeNull();
        target.Any.Should().BeNull();
        target.One.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Target_With_Any_Events()
    {
        //arrange
        var typeKey = "type";
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.Any().Event(f => f.With(typeKey, eventType));

        //act
        var target = builder.Build();

        //assert
        target.Any.Should().NotBeNull();
        target.All.Should().BeNull();
        target.One.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Target_With_One_Event()
    {
        //arrange
        var typeKey = "type";
        var eventType = JsonValue.Create("com.test");
        var builder = new ListenerTargetDefinitionBuilder();
        builder.One().With(typeKey, eventType);

        //act
        var target = builder.Build();

        //assert
        target.One.Should().NotBeNull();
        target.All.Should().BeNull();
        target.Any.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Strategy()
    {
        //arrange
        var builder = new ListenerTargetDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
