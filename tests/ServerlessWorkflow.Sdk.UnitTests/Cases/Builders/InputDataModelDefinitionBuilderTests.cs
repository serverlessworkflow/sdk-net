namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class InputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Input_With_From_Expression()
    {
        //arrange
        var fromExpr = "${ .data }";

        //act
        var input = new InputDataModelDefinitionBuilder()
            .From(fromExpr)
            .Build();

        //assert
        input.From.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Input_With_Schema()
    {
        //arrange
        var format = "json";

        //act
        var input = new InputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();

        //assert
        input.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Input()
    {
        //arrange
        var builder = new InputDataModelDefinitionBuilder();

        //act
        var input = builder.Build();

        //assert
        input.Should().NotBeNull();
    }

}
