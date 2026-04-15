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

public class OutputDataModelDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Output_With_As_Expression()
    {
        //arrange
        var asExpr = "${ .result }";

        //act
        var output = new OutputDataModelDefinitionBuilder()
            .As(asExpr)
            .Build();

        //assert
        output.As.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Output_With_Schema()
    {
        //arrange
        var format = "json";

        //act
        var output = new OutputDataModelDefinitionBuilder()
            .WithSchema(s => s.WithFormat(format))
            .Build();

        //assert
        output.Schema.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Empty_Output()
    {
        //arrange
        var builder = new OutputDataModelDefinitionBuilder();

        //act
        var output = builder.Build();

        //assert
        output.Should().NotBeNull();
    }

}
