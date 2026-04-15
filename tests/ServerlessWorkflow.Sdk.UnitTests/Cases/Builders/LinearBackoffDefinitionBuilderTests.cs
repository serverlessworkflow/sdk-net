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

public class LinearBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);

        //act
        var definition = new LinearBackoffDefinitionBuilder(increment).Build();

        //assert
        definition.Should().NotBeNull();
        definition.Increment.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_Without_Increment()
    {
        //arrange
        var builder = new LinearBackoffDefinitionBuilder();

        //act
        var definition = builder.Build();

        //assert
        definition.Should().NotBeNull();
    }

}
