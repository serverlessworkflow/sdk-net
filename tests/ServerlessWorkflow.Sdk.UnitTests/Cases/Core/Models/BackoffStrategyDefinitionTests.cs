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

public class BackoffStrategyDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Constant_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateConstant();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Constant_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateConstant();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<BackoffStrategyDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Exponential_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateExponential();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Linear_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateLinear();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }
}
