namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExtensionDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Extension_With_Extend_Type()
    {
        var extendType = "call";
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .Build();
        extension.Extend.Should().Be(extendType);
    }

    [Fact]
    public void Build_Should_Create_Extension_With_When_Condition()
    {
        var extendType = "all";
        var whenExpr = "${ .logging }";
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .When(whenExpr)
            .Build();
        extension.Extend.Should().Be(extendType);
        extension.When.Should().Be(whenExpr);
    }

    [Fact]
    public void Build_Should_Throw_When_Extend_Missing()
    {
        var act = () => new ExtensionDefinitionBuilder().Build();
        act.Should().Throw<ArgumentException>();
    }

}
