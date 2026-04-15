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

public class ProcessTypeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Container_Json_Should_Work()
    {
        //arrange
        var toSerialize = ProcessTypeDefinitionFactory.CreateContainer();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ProcessTypeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ProcessTypeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Container_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ProcessTypeDefinitionFactory.CreateContainer();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ProcessTypeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Shell_Json_Should_Work()
    {
        //arrange
        var toSerialize = ProcessTypeDefinitionFactory.CreateShell();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ProcessTypeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ProcessTypeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Script_Json_Should_Work()
    {
        //arrange
        var toSerialize = ProcessTypeDefinitionFactory.CreateScript();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ProcessTypeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ProcessTypeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Workflow_Json_Should_Work()
    {
        //arrange
        var toSerialize = ProcessTypeDefinitionFactory.CreateWorkflow();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ProcessTypeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ProcessTypeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void ProcessType_Should_Return_Container_When_Container_Is_Set()
    {
        //arrange
        var definition = ProcessTypeDefinitionFactory.CreateContainer();
        //act
        var processType = definition.ProcessType;
        //assert
        processType.Should().Be(ProcessType.Container);
    }

    [Fact]
    public void ProcessType_Should_Return_Shell_When_Shell_Is_Set()
    {
        //arrange
        var definition = ProcessTypeDefinitionFactory.CreateShell();
        //act
        var processType = definition.ProcessType;
        //assert
        processType.Should().Be(ProcessType.Shell);
    }

    [Fact]
    public void ProcessType_Should_Return_Script_When_Script_Is_Set()
    {
        //arrange
        var definition = ProcessTypeDefinitionFactory.CreateScript();
        //act
        var processType = definition.ProcessType;
        //assert
        processType.Should().Be(ProcessType.Script);
    }

    [Fact]
    public void ProcessType_Should_Return_Workflow_When_Workflow_Is_Set()
    {
        //arrange
        var definition = ProcessTypeDefinitionFactory.CreateWorkflow();
        //act
        var processType = definition.ProcessType;
        //assert
        processType.Should().Be(ProcessType.Workflow);
    }
}
