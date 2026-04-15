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

public class RunTaskDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Container_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateContainer();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Container_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateContainer();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<RunTaskDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Shell_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateShell();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Script_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateScript();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Workflow_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateWorkflow();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
