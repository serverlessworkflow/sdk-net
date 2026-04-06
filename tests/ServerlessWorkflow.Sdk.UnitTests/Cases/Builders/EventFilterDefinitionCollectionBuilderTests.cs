namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EventFilterDefinitionCollectionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Collection_With_Single_Filter()
    {
        //arrange
        var attrName = "type";
        var attrValue = JsonValue.Create("com.example.test");
        var expectedCount = 1;

        //act
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With(attrName, attrValue))
            .Build();

        //assert
        collection.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Create_Collection_With_Multiple_Filters()
    {
        //arrange
        var typeKey = "type";
        var typeA = JsonValue.Create("com.a");
        var typeB = JsonValue.Create("com.b");
        var expectedCount = 2;

        //act
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With(typeKey, typeA))
            .Event(f => f.With(typeKey, typeB))
            .Build();

        //assert
        collection.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Throw_When_No_Filters()
    {
        //arrange
        var builder = new EventFilterDefinitionCollectionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
