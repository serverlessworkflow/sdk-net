namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OutputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Output_With_As_Expression()
    {
        //arrange
        var asExpr = "${ .result }";

        //act
        var output = new OutputDataModelDefinitionBuilder()
            .As(asExpr)
            .Build();

        //assert
        output.As.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Output_With_Schema()
    {
        //arrange
        var format = "json";

        //act
        var output = new OutputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();

        //assert
        output.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Output()
    {
        //arrange
        var builder = new OutputDataModelDefinitionBuilder();

        //act
        var output = builder.Build();

        //assert
        output.Should().NotBeNull();
    }

}
