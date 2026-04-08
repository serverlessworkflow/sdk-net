namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ListenerDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Listener_With_One_Event()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.example.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));

        //act
        var result = builder.Build();

        //assert
        result.To.One.Should().NotBeNull();
        result.To.One.With?[typeKey]?.GetValue<string>().Should().Be(typeValue);
    }

    [Fact]
    public void Build_Should_Create_Listener_With_Read_Mode()
    {
        //arrange
        var readMode = EventReadMode.Envelope;
        var typeKey = "type";
        var typeValue = "com.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));
        builder.Read(readMode);

        //act
        var result = builder.Build();

        //assert
        result.Read.Should().Be(readMode);
        result.To.One.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Target()
    {
        //arrange
        var builder = new ListenerDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
