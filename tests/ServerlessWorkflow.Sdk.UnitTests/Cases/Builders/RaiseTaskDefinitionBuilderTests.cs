namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RaiseTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Error_Via_Builder()
    {
        var errorType = "https://errors.com/not-found";
        var errorTitle = "Not Found";
        var errorStatus = "404";
        var task = new RaiseTaskDefinitionBuilder()
            .Error(e => e.WithType(errorType).WithTitle(errorTitle).WithStatus(errorStatus))
            .Build();
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorType);
        error!.Title.Should().Be(errorTitle);
        error!.Status.Should().Be(errorStatus);
    }

    [Fact]
    public void Build_Should_Set_Error_Via_Definition()
    {
        var errorDef = new ErrorDefinition { Type = "https://err.com/t", Title = "T", Status = "500" };
        var task = new RaiseTaskDefinitionBuilder()
            .Error(errorDef)
            .Build();
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorDef.Type);
    }

    [Fact]
    public void Build_Should_Throw_When_Error_Missing()
    {
        var act = () => new RaiseTaskDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
