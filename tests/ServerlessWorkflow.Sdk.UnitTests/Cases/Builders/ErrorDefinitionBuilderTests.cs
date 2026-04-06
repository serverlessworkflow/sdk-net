namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ErrorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Error_With_All_Properties()
    {
        //arrange
        var type = "https://errors.com/not-found";
        var title = "Not Found";
        var status = "404";
        var detail = "Resource not found";
        var instance = "/items/123";

        //act
        var error = new ErrorDefinitionBuilder()
            .WithType(type)
            .WithTitle(title)
            .WithStatus(status)
            .WithDetail(detail)
            .WithInstance(instance)
            .Build();

        //assert
        error.Type.Should().Be(type);
        error.Title.Should().Be(title);
        error.Status.Should().Be(status);
        error.Detail.Should().Be(detail);
        error.Instance.Should().Be(instance);
    }

    [Fact]
    public void Build_Should_Throw_When_Type_Missing()
    {
        //arrange
        var title = "t";
        var status = "400";

        //act
        var act = () => new ErrorDefinitionBuilder().WithTitle(title).WithStatus(status).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Title_Missing()
    {
        //arrange
        var type = "t";
        var status = "400";

        //act
        var act = () => new ErrorDefinitionBuilder().WithType(type).WithStatus(status).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Status_Missing()
    {
        //arrange
        var type = "t";
        var title = "t";

        //act
        var act = () => new ErrorDefinitionBuilder().WithType(type).WithTitle(title).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
