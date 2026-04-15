// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
