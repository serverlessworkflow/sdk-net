namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExtensionDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Extension_With_Extend_Type()
    {
        //arrange
        var extendType = "call";

        //act
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .Build();

        //assert
        extension.Extend.Should().Be(extendType);
    }

    [Fact]
    public void Build_Should_Create_Extension_With_When_Condition()
    {
        //arrange
        var extendType = "all";
        var whenExpr = "${ .logging }";

        //act
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .When(whenExpr)
            .Build();

        //assert
        extension.Extend.Should().Be(extendType);
        extension.When.Should().Be(whenExpr);
    }

    [Fact]
    public void Build_Should_Throw_When_Extend_Missing()
    {
        //arrange
        var builder = new ExtensionDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<ArgumentException>();
    }

}
