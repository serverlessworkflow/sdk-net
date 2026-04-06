namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class JitterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Jitter_With_Range()
    {
        var from = Duration.FromMilliseconds(100);
        var to = Duration.FromMilliseconds(500);
        var definition = new JitterDefinitionBuilder()
            .From(from)
            .To(to)
            .Build();
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

    [Fact]
    public void Build_Should_Create_Jitter_From_Constructor()
    {
        var from = Duration.FromMilliseconds(50);
        var to = Duration.FromMilliseconds(200);
        var definition = new JitterDefinitionBuilder(from, to).Build();
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

}
