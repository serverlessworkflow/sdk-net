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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public sealed class JQRuntimeExpressionEvaluatorTests 
    : IRuntimeExpressionEvaluatorTests
{

    protected override IRuntimeExpressionEvaluator ExpressionEvaluator { get; } = new JQRuntimeExpressionEvaluator();

    [Fact]
    public override async Task Evaluate_Expression_Should_Work()
    {
        //arrange
        var expression = "1 + 2";
        //act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, new JsonObject(), null, TestContext.Current.CancellationToken);
        //assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_Expression_Against_Input_Should_Work()
    {
        //arrange
        var expression = ".value + 2";
        var input = new JsonObject
        {
            ["value"] = 1
        };
        //act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, input, null, TestContext.Current.CancellationToken);
        //assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_Expression_Against_Arguments_Should_Work()
    {
        //arrange
        var arguments = new JsonObject()
        {
            ["ARG1"] = 2
        };
        var input = new JsonObject
        {
            ["value"] = 1
        };
        var expression = ".value + $ARG1";
        //act
        var result = await ExpressionEvaluator.EvaluateAsync(expression, input, arguments, TestContext.Current.CancellationToken);
        //assert
        result.Should().NotBeNull();
        result.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

    [Fact]
    public override async Task Evaluate_JsonObject_Should_Work()
    {
        //arrange
        var propertyName = "additionResult";
        var arguments = new JsonObject()
        {
            ["ARG1"] = 2
        };
        var input = new JsonObject
        {
            ["value"] = 1
        };
        var value = new JsonObject()
        {
            [propertyName] = "${ .value + $ARG1 }"
        };
        //act
        var result = await ExpressionEvaluator.EvaluateAsync(value, input, arguments, TestContext.Current.CancellationToken);
        //assert
        result.Should().NotBeNull();
        result.AsObject().TryGetPropertyValue(propertyName, out var propertyValue).Should().BeTrue();
        propertyValue.Should().NotBeNull();
        propertyValue.AsValue().TryGetValue<int>(out var additionResult).Should().BeTrue();
        additionResult.Should().Be(3);
    }

}
