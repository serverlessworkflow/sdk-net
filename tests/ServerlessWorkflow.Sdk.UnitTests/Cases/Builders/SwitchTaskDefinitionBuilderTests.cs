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

public class SwitchTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Switch_With_Cases()
    {
        //arrange
        var activeCaseName = "active";
        var defaultCaseName = "default";
        var whenExpr = "${ .status == \"active\" }";
        var expectedCount = 2;

        //act
        var task = new SwitchTaskDefinitionBuilder()
            .Case(activeCaseName, c => c.When(whenExpr).Then(FlowDirective.Continue))
            .Case(defaultCaseName, c => c.Then(FlowDirective.End))
            .Build();

        //assert
        task.Switch.Should().HaveCount(expectedCount);
        task.Switch[activeCaseName].When.Should().Be(whenExpr);
        task.Switch[activeCaseName].Then.Should().Be(FlowDirective.Continue);
        task.Switch[defaultCaseName].When.Should().BeNull();
        task.Switch[defaultCaseName].Then.Should().Be(FlowDirective.End);
    }

}
