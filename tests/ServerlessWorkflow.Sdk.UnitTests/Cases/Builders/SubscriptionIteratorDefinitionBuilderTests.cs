namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SubscriptionIteratorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Iterator_With_Item_And_At()
    {
        var itemVar = "event";
        var atVar = "index";
        var iterator = new SubscriptionIteratorDefinitionBuilder()
            .Item(itemVar)
            .At(atVar)
            .Build();
        iterator.Item.Should().Be(itemVar);
        iterator.At.Should().Be(atVar);
    }

    [Fact]
    public void Build_Should_Create_Empty_Iterator()
    {
        var iterator = new SubscriptionIteratorDefinitionBuilder().Build();
        iterator.Should().NotBeNull();
    }

}
