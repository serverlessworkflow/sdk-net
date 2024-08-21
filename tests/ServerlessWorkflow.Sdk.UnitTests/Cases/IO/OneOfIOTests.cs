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

using Microsoft.Extensions.DependencyInjection;
using Neuroglia.Serialization;
using ServerlessWorkflow.Sdk.IO;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.IO;

public class OneOfIOTests
    : IDisposable
{

    public OneOfIOTests()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflowIO();
        this.ServiceProvider = services.BuildServiceProvider();
    }

    protected ServiceProvider ServiceProvider { get; }

    protected IJsonSerializer JsonSerializer => this.ServiceProvider.GetRequiredService<IJsonSerializer>();

    protected IYamlSerializer YamlSerializer => this.ServiceProvider.GetRequiredService<IYamlSerializer>();

    [Fact]
    public void Serialize_And_Deserialize_OneOf_ToFromJson_Should_Work()
    {
        //arrange
        var uri = new Uri("https://test.com");
        var endpoint = new EndpointDefinition() { Uri = uri };
        var t1OneOf = new OneOf<EndpointDefinition, Uri>(endpoint);
        var t2OneOf = new OneOf<EndpointDefinition, Uri>(uri);

        //act
        var t1OneOfJson = this.JsonSerializer.SerializeToText(t1OneOf);
        var t2OneOfJson = this.JsonSerializer.SerializeToText(t2OneOf);
        var t1OneOfDeserialized = this.JsonSerializer.Deserialize<OneOf<EndpointDefinition, Uri>>(t1OneOfJson);
        var t2OneOfDeserialized = this.JsonSerializer.Deserialize<OneOf<EndpointDefinition, Uri>>(t2OneOfJson);

        //assert
        t1OneOfJson.Should().Be(this.JsonSerializer.SerializeToText(endpoint));
        t2OneOfJson.Should().Be(this.JsonSerializer.SerializeToText(uri));
        t1OneOfDeserialized.Should().BeEquivalentTo(t1OneOf);
        t2OneOfDeserialized.Should().BeEquivalentTo(t2OneOf);
    }

    [Fact]
    public void Serialize_And_Deserialize_OneOf_ToFromYaml_Should_Work()
    {
        //arrange
        var uri = new Uri("https://test.com");
        var endpoint = new EndpointDefinition() { Uri = uri };
        var t1OneOf = new OneOf<EndpointDefinition, Uri>(endpoint);
        var t2OneOf = new OneOf<EndpointDefinition, Uri>(uri);

        //act
        var t1OneOfYaml = this.YamlSerializer.SerializeToText(t1OneOf);
        var t2OneOfYaml = this.YamlSerializer.SerializeToText(t2OneOf);
        var t1OneOfDeserialized = this.YamlSerializer.Deserialize<OneOf<EndpointDefinition, Uri>>(t1OneOfYaml);
        var t2OneOfDeserialized = this.YamlSerializer.Deserialize<OneOf<EndpointDefinition, Uri>>(t2OneOfYaml);

        //assert
        t1OneOfYaml.Should().Be(this.YamlSerializer.SerializeToText(endpoint));
        t2OneOfYaml.Should().Be(this.YamlSerializer.SerializeToText(uri));
        t1OneOfDeserialized.Should().BeEquivalentTo(t1OneOf);
        t2OneOfDeserialized.Should().BeEquivalentTo(t2OneOf);
    }

    void IDisposable.Dispose()
    {
        this.ServiceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

}

