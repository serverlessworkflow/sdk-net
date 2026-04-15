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

public class OAuth2AuthenticationEndpointsDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Endpoints_With_Custom_Uris()
    {
        //arrange
        var tokenUri = new Uri("/custom/token", UriKind.Relative);
        var revocationUri = new Uri("/custom/revoke", UriKind.Relative);
        var introspectionUri = new Uri("/custom/introspect", UriKind.Relative);

        //act
        var endpoints = new OAuth2AuthenticationEndpointsDefinitionBuilder()
            .WithTokenEndpoint(tokenUri)
            .WithRevocationEndpoint(revocationUri)
            .WithIntrospectionEndpoint(introspectionUri)
            .Build();

        //assert
        endpoints.Token.Should().Be(tokenUri);
        endpoints.Revocation.Should().Be(revocationUri);
        endpoints.Introspection.Should().Be(introspectionUri);
    }

    [Fact]
    public void Build_Should_Use_Default_Endpoints_When_Not_Configured()
    {
        //arrange
        var builder = new OAuth2AuthenticationEndpointsDefinitionBuilder();

        //act
        var endpoints = builder.Build();

        //assert
        endpoints.Token.Should().NotBeNull();
        endpoints.Revocation.Should().NotBeNull();
        endpoints.Introspection.Should().NotBeNull();
    }

}
