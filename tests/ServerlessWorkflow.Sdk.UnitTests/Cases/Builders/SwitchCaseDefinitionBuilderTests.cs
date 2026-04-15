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

public class SwitchCaseDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Case_With_When_And_Then()
    {
        //arrange
        var whenExpr = "${ .status == \"active\" }";

        //act
        var @case = new SwitchCaseDefinitionBuilder()
            .When(whenExpr)
            .Then(FlowDirective.Continue)
            .Build();

        //assert
        @case.When.Should().Be(whenExpr);
        @case.Then.Should().Be(FlowDirective.Continue);
    }

    [Fact]
    public void Build_Should_Throw_When_Then_Missing()
    {
        //arrange
        var whenExpr = "${ true }";

        //act
        var act = () => new SwitchCaseDefinitionBuilder().When(whenExpr).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Create_Default_Case_Without_When()
    {
        //arrange
        var builder = new SwitchCaseDefinitionBuilder();

        //act
        var @case = builder
            .Then(FlowDirective.End)
            .Build();

        //assert
        @case.When.Should().BeNull();
        @case.Then.Should().Be(FlowDirective.End);
    }

}
