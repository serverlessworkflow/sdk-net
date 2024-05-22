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

using ServerlessWorkflow.Sdk.IO;
using Neuroglia.Serialization.Json;
using Neuroglia.Serialization.Yaml;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.IO;

public class WorkflowDefinitionReaderTests
{

    [Fact]
    public async Task Read_Workflow_Definition_From_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.Create();
        using var stream = new MemoryStream();
        JsonSerializer.Default.Serialize(toSerialize, stream);
        await stream.FlushAsync();
        stream.Position = 0;
        var reader = WorkflowDefinitionReader.Create();

        //act
        var deserialized = await reader.ReadAsync(stream);

        //assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public async Task Read_Workflow_Definition_From_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.Create();
        using var stream = new MemoryStream();
        YamlSerializer.Default.Serialize(toSerialize, stream);
        await stream.FlushAsync();
        stream.Position = 0;
        var reader = WorkflowDefinitionReader.Create();

        //act
        var deserialized = await reader.ReadAsync(stream);

        //assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

}

