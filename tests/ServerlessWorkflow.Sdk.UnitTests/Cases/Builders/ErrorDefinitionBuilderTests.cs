namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ErrorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Error_With_All_Properties()
    {
        var type = "https://errors.com/not-found";
        var title = "Not Found";
        var status = "404";
        var detail = "Resource not found";
        var instance = "/items/123";
        var error = new ErrorDefinitionBuilder()
            .WithType(type)
            .WithTitle(title)
            .WithStatus(status)
            .WithDetail(detail)
            .WithInstance(instance)
            .Build();
        error.Type.Should().Be(type);
        error.Title.Should().Be(title);
        error.Status.Should().Be(status);
        error.Detail.Should().Be(detail);
        error.Instance.Should().Be(instance);
    }

    [Fact]
    public void Build_Should_Throw_When_Type_Missing()
    {
        var act = () => new ErrorDefinitionBuilder().WithTitle("t").WithStatus("400").Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Title_Missing()
    {
        var act = () => new ErrorDefinitionBuilder().WithType("t").WithStatus("400").Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Status_Missing()
    {
        var act = () => new ErrorDefinitionBuilder().WithType("t").WithTitle("t").Build();
        act.Should().Throw<NullReferenceException>();
    }

}
