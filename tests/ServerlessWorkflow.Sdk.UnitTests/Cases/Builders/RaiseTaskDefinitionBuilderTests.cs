namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RaiseTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Error_Via_Builder()
    {
        //arrange
        var errorType = "https://errors.com/not-found";
        var errorTitle = "Not Found";
        var errorStatus = "404";

        //act
        var task = new RaiseTaskDefinitionBuilder()
            .Error(e => e.WithType(errorType).WithTitle(errorTitle).WithStatus(errorStatus))
            .Build();

        //assert
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorType);
        error!.Title.Should().Be(errorTitle);
        error!.Status.Should().Be(errorStatus);
    }

    [Fact]
    public void Build_Should_Set_Error_Via_Definition()
    {
        //arrange
        var errorType = "https://err.com/t";
        var errorTitle = "T";
        var errorStatus = "500";
        var errorDef = new ErrorDefinition { Type = errorType, Title = errorTitle, Status = errorStatus };

        //act
        var task = new RaiseTaskDefinitionBuilder()
            .Error(errorDef)
            .Build();

        //assert
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorType);
    }

    [Fact]
    public void Build_Should_Throw_When_Error_Missing()
    {
        //arrange
        var builder = new RaiseTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
