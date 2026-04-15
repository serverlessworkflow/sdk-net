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

public class EventFilterDefinitionCollectionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Collection_With_Single_Filter()
    {
        //arrange
        var attrName = "type";
        var attrValue = JsonValue.Create("com.example.test");
        var expectedCount = 1;

        //act
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With(attrName, attrValue))
            .Build();

        //assert
        collection.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Create_Collection_With_Multiple_Filters()
    {
        //arrange
        var typeKey = "type";
        var typeA = JsonValue.Create("com.a");
        var typeB = JsonValue.Create("com.b");
        var expectedCount = 2;

        //act
        var collection = new EventFilterDefinitionCollectionBuilder()
            .Event(f => f.With(typeKey, typeA))
            .Event(f => f.With(typeKey, typeB))
            .Build();

        //assert
        collection.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Throw_When_No_Filters()
    {
        //arrange
        var builder = new EventFilterDefinitionCollectionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
