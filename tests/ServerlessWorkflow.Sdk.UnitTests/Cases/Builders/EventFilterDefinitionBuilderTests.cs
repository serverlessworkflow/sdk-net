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

public class EventFilterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Filter_With_Attributes()
    {
        //arrange
        var attrName = "type";
        var attrValue = "com.example.test";

        //act
        var filter = new EventFilterDefinitionBuilder()
            .With(attrName, JsonValue.Create(attrValue))
            .Build();

        //assert
        filter.With?[attrName]?.GetValue<string>().Should().Be(attrValue);
    }

    [Fact]
    public void Build_Should_Create_Filter_With_Prebuilt_Attributes()
    {
        //arrange
        var sourceKey = "source";
        var sourceValue = "https://example.com";
        var attributes = new JsonObject { [sourceKey] = sourceValue };

        //act
        var filter = new EventFilterDefinitionBuilder()
            .With(attributes)
            .Build();

        //assert
        filter.With?[sourceKey]?.GetValue<string>().Should().Be(sourceValue);
    }

}
