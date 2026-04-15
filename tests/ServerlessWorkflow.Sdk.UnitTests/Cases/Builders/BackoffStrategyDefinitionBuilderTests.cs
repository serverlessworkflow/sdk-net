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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BackoffStrategyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Constant_Backoff()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Constant();

        //act
        var definition = builder.Build();

        //assert
        definition.Constant.Should().NotBeNull();
        definition.Exponential.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Exponential_Backoff()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Exponential();

        //act
        var definition = builder.Build();

        //assert
        definition.Exponential.Should().NotBeNull();
        definition.Constant.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Linear(increment);

        //act
        var definition = builder.Build();

        //assert
        definition.Linear.Should().NotBeNull();
        definition.Linear!.Increment.Should().Be(increment);
        definition.Constant.Should().BeNull();
        definition.Exponential.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Strategy_Configured()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
