namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class JitterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Jitter_With_Range()
    {
        //arrange
        var from = Duration.FromMilliseconds(100);
        var to = Duration.FromMilliseconds(500);

        //act
        var definition = new JitterDefinitionBuilder()
            .From(from)
            .To(to)
            .Build();

        //assert
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

    [Fact]
    public void Build_Should_Create_Jitter_From_Constructor()
    {
        //arrange
        var from = Duration.FromMilliseconds(50);
        var to = Duration.FromMilliseconds(200);

        //act
        var definition = new JitterDefinitionBuilder(from, to).Build();

        //assert
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

}
