namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OutputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Output_With_As_Expression()
    {
        var asExpr = "${ .result }";
        var output = new OutputDataModelDefinitionBuilder()
            .As(asExpr)
            .Build();
        output.As.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Output_With_Schema()
    {
        var format = "json";
        var output = new OutputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();
        output.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Output()
    {
        var output = new OutputDataModelDefinitionBuilder().Build();
        output.Should().NotBeNull();
    }

}
