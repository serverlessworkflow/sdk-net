namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class WorkflowDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Minimal_Workflow()
    {
        // act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName("test-workflow")
            .WithVersion("1.0.0")
            .Do("greet", task => task.Set("message", "hello"))
            .Build();
        // assert
        workflow.Should().NotBeNull();
        workflow.Document.Name.Should().Be("test-workflow");
        workflow.Document.Version.Should().Be("1.0.0");
        workflow.Document.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
        workflow.Do.Should().HaveCount(1);
    }

    [Fact]
    public void Build_Should_Set_All_Document_Properties()
    {
        // act
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl("1.0.0")
            .WithNamespace("my-namespace")
            .WithName("my-workflow")
            .WithVersion("2.0.0")
            .WithTitle("My Workflow")
            .WithSummary("A test workflow")
            .WithTag("env", "test")
            .Do("step1", task => task.Set("k", "v"))
            .Build();
        // assert
        workflow.Document.Dsl.Should().Be("1.0.0");
        workflow.Document.Namespace.Should().Be("my-namespace");
        workflow.Document.Name.Should().Be("my-workflow");
        workflow.Document.Version.Should().Be("2.0.0");
        workflow.Document.Title.Should().Be("My Workflow");
        workflow.Document.Summary.Should().Be("A test workflow");
        workflow.Document.Tags.Should().ContainKey("env");
    }

    [Fact]
    public void Build_Should_Throw_When_Name_Missing()
    {
        // arrange
        var builder = new WorkflowDefinitionBuilder()
            .WithVersion("1.0.0")
            .Do("step", task => task.Set("k", "v"));
        // act
        var act = () => builder.Build();
        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Version_Missing()
    {
        // arrange
        var builder = new WorkflowDefinitionBuilder()
            .WithName("test")
            .Do("step", task => task.Set("k", "v"));
        // act
        var act = () => builder.Build();
        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Tasks()
    {
        // arrange
        var builder = new WorkflowDefinitionBuilder()
            .WithName("test")
            .WithVersion("1.0.0");
        // act
        var act = () => builder.Build();
        // assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void WithVersion_Should_Throw_For_Invalid_SemVer()
    {
        // act
        var act = () => new WorkflowDefinitionBuilder().WithVersion("not-semver");
        // assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void WithName_Should_Throw_For_Invalid_Name()
    {
        // act
        var act = () => new WorkflowDefinitionBuilder().WithName("INVALID NAME!");
        // assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Build_Should_Configure_Timeout()
    {
        // act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName("test")
            .WithVersion("1.0.0")
            .WithTimeout(timeout => timeout.After(Duration.FromSeconds(30)))
            .Do("step", task => task.Set("k", "v"))
            .Build();
        // assert
        workflow.Timeout.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Configure_Components()
    {
        // act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName("test")
            .WithVersion("1.0.0")
            .UseSecret("my-secret")
            .UseSecret("secret1")
            .UseSecret("secret2")
            .Do("step", task => task.Set("k", "v"))
            .Build();
        // assert
        workflow.Use.Should().NotBeNull();
        workflow.Use!.Secrets.Should().Contain("my-secret");
        workflow.Use.Secrets.Should().Contain("secret1");
    }

}
