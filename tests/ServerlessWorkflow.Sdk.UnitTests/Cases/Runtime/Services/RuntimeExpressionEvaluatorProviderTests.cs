using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class RuntimeExpressionEvaluatorProviderTests
{

    [Fact]
    public void GetEvaluator_Should_Return_Evaluator_That_Supports_Language()
    {
        //arrange
        var language = "jq";
        var mockEvaluator = new Mock<IRuntimeExpressionEvaluator>();
        mockEvaluator.Setup(e => e.Supports(language)).Returns(true);
        var provider = new RuntimeExpressionEvaluatorProvider([mockEvaluator.Object]);

        //act
        var evaluator = provider.GetEvaluator(language);

        //assert
        evaluator.Should().Be(mockEvaluator.Object);
    }

    [Fact]
    public void GetEvaluator_Should_Return_Null_When_No_Evaluator_Supports_Language()
    {
        //arrange
        var language = "unknown";
        var mockEvaluator = new Mock<IRuntimeExpressionEvaluator>();
        mockEvaluator.Setup(e => e.Supports(It.IsAny<string>())).Returns(false);
        var provider = new RuntimeExpressionEvaluatorProvider([mockEvaluator.Object]);

        //act
        var evaluator = provider.GetEvaluator(language);

        //assert
        evaluator.Should().BeNull();
    }

}
