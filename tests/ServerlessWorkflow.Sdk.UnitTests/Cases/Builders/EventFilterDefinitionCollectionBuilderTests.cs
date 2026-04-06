namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EventFilterDefinitionCollectionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Collection_With_Single_Filter()
    {
        var attrName = "type";
        var attrValue = JsonValue.Create("com.example.test");
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With(attrName, attrValue))
            .Build();
        collection.Should().HaveCount(1);
    }

    [Fact]
    public void Build_Should_Create_Collection_With_Multiple_Filters()
    {
        var typeA = JsonValue.Create("com.a");
        var typeB = JsonValue.Create("com.b");
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With("type", typeA))
            .Event(f => f.With("type", typeB))
            .Build();
        collection.Should().HaveCount(2);
    }

    [Fact]
    public void Build_Should_Throw_When_No_Filters()
    {
        var act = () => new EventFilterDefinitionCollectionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
