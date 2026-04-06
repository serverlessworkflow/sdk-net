namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class InputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Input_With_From_Expression()
    {
        var fromExpr = "${ .data }";
        var input = new InputDataModelDefinitionBuilder()
            .From(fromExpr)
            .Build();
        input.From.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Input_With_Schema()
    {
        var format = "json";
        var input = new InputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();
        input.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Input()
    {
        var input = new InputDataModelDefinitionBuilder().Build();
        input.Should().NotBeNull();
    }

}
