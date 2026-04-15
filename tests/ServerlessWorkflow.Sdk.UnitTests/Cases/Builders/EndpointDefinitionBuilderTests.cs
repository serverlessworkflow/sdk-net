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

public class EndpointDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Endpoint_With_Uri()
    {
        //arrange
        var uri = new Uri("https://api.example.com/v1");

        //act
        var endpoint = new EndpointDefinitionBuilder()
            .WithUri(uri)
            .Build();

        //assert
        endpoint.Uri.Should().Be(uri);
    }

    [Fact]
    public void Build_Should_Create_Endpoint_With_Authentication()
    {
        //arrange
        var uri = new Uri("https://api.example.com/v1");
        var token = "my-token";

        //act
        var endpoint = new EndpointDefinitionBuilder()
            .WithUri(uri)
            .UseAuthentication(auth => auth.Bearer().WithToken(token))
            .Build();

        //assert
        endpoint.Uri.Should().Be(uri);
        endpoint.Authentication.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Uri_Missing()
    {
        //arrange
        var builder = new EndpointDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
