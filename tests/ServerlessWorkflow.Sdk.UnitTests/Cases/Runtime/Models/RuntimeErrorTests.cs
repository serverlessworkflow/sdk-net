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

using SdkJsonSerializationContext = ServerlessWorkflow.Sdk.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class RuntimeErrorTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = RuntimeErrorFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, SdkJsonSerializationContext.Default.Error);
        var deserialized = JsonSerializer.Deserialize(json, SdkJsonSerializationContext.Default.Error);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = RuntimeErrorFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, SdkJsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<Error>(yaml, SdkJsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Communication_Factory_Should_Create_Communication_Error()
    {
        //arrange
        var instance = new Uri("/tasks/456", UriKind.RelativeOrAbsolute);
        //act
        var error = Error.Communication(instance, 502, "Bad Gateway");
        //assert
        error.Type.Should().Be(ErrorType.Communication);
        error.Title.Should().Be(ErrorTitle.Communication);
        error.Status.Should().Be(502);
        error.Detail.Should().Be("Bad Gateway");
        error.Instance.Should().Be(instance);
    }

    [Fact]
    public void Runtime_Factory_Should_Create_Runtime_Error()
    {
        //arrange
        var instance = new Uri("/tasks/789", UriKind.RelativeOrAbsolute);
        //act
        var error = Error.Runtime(instance, "Something went wrong");
        //assert
        error.Type.Should().Be(ErrorType.Runtime);
        error.Title.Should().Be(ErrorTitle.Runtime);
        error.Status.Should().Be(ErrorStatus.Runtime);
        error.Detail.Should().Be("Something went wrong");
    }

    [Fact]
    public void Validation_Factory_Should_Create_Validation_Error()
    {
        //arrange
        var instance = new Uri("/tasks/abc", UriKind.RelativeOrAbsolute);
        //act
        var error = Error.Validation(instance, "Invalid input");
        //assert
        error.Type.Should().Be(ErrorType.Validation);
        error.Status.Should().Be(ErrorStatus.Validation);
    }

    [Fact]
    public void Configuration_Factory_Should_Create_Configuration_Error()
    {
        //arrange
        var instance = new Uri("/tasks/def", UriKind.RelativeOrAbsolute);
        //act
        var error = Error.Configuration(instance, "Missing config");
        //assert
        error.Type.Should().Be(ErrorType.Configuration);
        error.Status.Should().Be(ErrorStatus.Configuration);
    }

}
