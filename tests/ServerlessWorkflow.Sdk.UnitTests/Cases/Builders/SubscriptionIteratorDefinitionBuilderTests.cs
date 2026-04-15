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

public class SubscriptionIteratorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Iterator_With_Item_And_At()
    {
        //arrange
        var itemVar = "event";
        var atVar = "index";

        //act
        var iterator = new SubscriptionIteratorDefinitionBuilder()
            .Item(itemVar)
            .At(atVar)
            .Build();

        //assert
        iterator.Item.Should().Be(itemVar);
        iterator.At.Should().Be(atVar);
    }

    [Fact]
    public void Build_Should_Create_Empty_Iterator()
    {
        //arrange
        var builder = new SubscriptionIteratorDefinitionBuilder();

        //act
        var iterator = builder.Build();

        //assert
        iterator.Should().NotBeNull();
    }

}
