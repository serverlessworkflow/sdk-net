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

public class EventDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Event_With_Attributes()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.example.test";
        var sourceKey = "source";
        var sourceValue = "https://example.com";

        //act
        var e = new EventDefinitionBuilder()
            .With(typeKey, JsonValue.Create(typeValue))
            .With(sourceKey, JsonValue.Create(sourceValue))
            .Build();

        //assert
        e.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        e.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Attributes()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.test";
        var sourceKey = "source";
        var sourceValue = "https://test.com";
        var attrs = new JsonObject { [typeKey] = typeValue, [sourceKey] = sourceValue };

        //act
        var e = new EventDefinitionBuilder().With(attrs).Build();

        //assert
        e.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        e.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

}
