namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ListenTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Listener_Target()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.test";

        //act
        var task = new ListenTaskDefinitionBuilder()
            .To(listener => listener.One().With(typeKey, JsonValue.Create(typeValue)))
            .Build();

        //assert
        task.Listen.To.One.Should().NotBeNull();
        task.Listen.To.One.With?[typeKey]?.GetValue<string>().Should().Be(typeValue);
    }

}
