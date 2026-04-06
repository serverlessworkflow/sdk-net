namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SubscriptionIteratorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Iterator_With_Item_And_At()
    {
        //arrange
        var itemVar = "event";
        var atVar = "index";

        //act
        var iterator = new SubscriptionIteratorDefinitionBuilder()
            .Item(itemVar)
            .At(atVar)
            .Build();

        //assert
        iterator.Item.Should().Be(itemVar);
        iterator.At.Should().Be(atVar);
    }

    [Fact]
    public void Build_Should_Create_Empty_Iterator()
    {
        //arrange
        var builder = new SubscriptionIteratorDefinitionBuilder();

        //act
        var iterator = builder.Build();

        //assert
        iterator.Should().NotBeNull();
    }

}
