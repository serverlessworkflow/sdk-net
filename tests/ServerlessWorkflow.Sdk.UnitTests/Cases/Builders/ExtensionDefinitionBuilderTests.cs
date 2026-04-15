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

public class ExtensionDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Extension_With_Extend_Type()
    {
        //arrange
        var extendType = "call";

        //act
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .Build();

        //assert
        extension.Extend.Should().Be(extendType);
    }

    [Fact]
    public void Build_Should_Create_Extension_With_When_Condition()
    {
        //arrange
        var extendType = "all";
        var whenExpr = "${ .logging }";

        //act
        var extension = new ExtensionDefinitionBuilder()
            .Extend(extendType)
            .When(whenExpr)
            .Build();

        //assert
        extension.Extend.Should().Be(extendType);
        extension.When.Should().Be(whenExpr);
    }

    [Fact]
    public void Build_Should_Throw_When_Extend_Missing()
    {
        //arrange
        var builder = new ExtensionDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<ArgumentException>();
    }

}
