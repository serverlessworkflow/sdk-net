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

public class EmitTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Event_Via_Builder()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.test";
        var sourceKey = "source";
        var sourceValue = "https://test.com";

        //act
        var task = new EmitTaskDefinitionBuilder()
            .Event(e => e.With(typeKey, JsonValue.Create(typeValue)).With(sourceKey, JsonValue.Create(sourceValue)))
            .Build();

        //assert
        task.Emit.Event.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        task.Emit.Event.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

    [Fact]
    public void Build_Should_Set_Event_Via_Definition()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.direct";
        var eventDef = new EventDefinition { With = new JsonObject { [typeKey] = typeValue } };

        //act
        var task = new EmitTaskDefinitionBuilder()
            .Event(eventDef)
            .Build();

        //assert
        task.Emit.Event.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Event_Missing()
    {
        //arrange
        var builder = new EmitTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
