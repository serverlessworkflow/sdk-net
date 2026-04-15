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

public class ErrorCatcherDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Catcher_With_All_Properties()
    {
        //arrange
        var asVar = "error";
        var whenExpr = "${ .status == 404 }";
        var exceptWhenExpr = "${ .ignore }";

        //act
        var catcher = new ErrorCatcherDefinitionBuilder()
            .As(asVar)
            .When(whenExpr)
            .ExceptWhen(exceptWhenExpr)
            .Build();

        //assert
        catcher.As.Should().Be(asVar);
        catcher.When.Should().Be(whenExpr);
        catcher.ExceptWhen.Should().Be(exceptWhenExpr);
    }

    [Fact]
    public void Build_Should_Configure_Do_Tasks()
    {
        //arrange
        var taskName = "log";
        var key = "logged";
        var value = "true";
        var expectedCount = 1;

        //act
        var catcher = new ErrorCatcherDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        catcher.Do.Should().HaveCount(expectedCount);
        catcher.Do!.Keys.Should().Contain(taskName);
    }

    [Fact]
    public void Build_Should_Create_Empty_Catcher_With_Null_Properties()
    {
        //arrange
        var builder = new ErrorCatcherDefinitionBuilder();

        //act
        var catcher = builder.Build();

        //assert
        catcher.As.Should().BeNull();
        catcher.When.Should().BeNull();
        catcher.ExceptWhen.Should().BeNull();
        catcher.Do.Should().BeNull();
        catcher.Errors.Should().BeNull();
    }

}
