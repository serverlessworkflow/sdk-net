namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ListenerDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Listener_With_One_Event()
    {
        var typeKey = "type";
        var typeValue = "com.example.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));
        var result = builder.Build();
        result.To.One.Should().NotBeNull();
        result.To.One!.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
    }

    [Fact]
    public void Build_Should_Create_Listener_With_Read_Mode()
    {
        var readMode = EventReadMode.Envelope;
        var typeKey = "type";
        var typeValue = "com.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));
        builder.Read(readMode);
        var result = builder.Build();
        result.Read.Should().Be(readMode);
        result.To.One.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Target()
    {
        var act = () => new ListenerDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
