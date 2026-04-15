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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class BackoffDefinitionTests
{

    [Fact]
    public void ConstantBackoffDefinition_Should_Be_BackoffDefinition()
    {
        //arrange & act
        var definition = new ConstantBackoffDefinition();

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
    }

    [Fact]
    public void ExponentialBackoffDefinition_Should_Be_BackoffDefinition()
    {
        //arrange & act
        var definition = new ExponentialBackoffDefinition();

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
    }

    [Fact]
    public void LinearBackoffDefinition_Should_Set_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);

        //act
        var definition = new LinearBackoffDefinition { Increment = increment };

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
        definition.Increment.Should().Be(increment);
    }

}
