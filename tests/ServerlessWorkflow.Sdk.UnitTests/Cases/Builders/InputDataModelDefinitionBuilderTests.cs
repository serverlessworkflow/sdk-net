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

public class InputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Input_With_From_Expression()
    {
        //arrange
        var fromExpr = "${ .data }";

        //act
        var input = new InputDataModelDefinitionBuilder()
            .From(fromExpr)
            .Build();

        //assert
        input.From.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Input_With_Schema()
    {
        //arrange
        var format = "json";

        //act
        var input = new InputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();

        //assert
        input.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Input()
    {
        //arrange
        var builder = new InputDataModelDefinitionBuilder();

        //act
        var input = builder.Build();

        //assert
        input.Should().NotBeNull();
    }

}
