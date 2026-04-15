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

public class OAuth2AuthenticationClientDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Client_With_All_Properties()
    {
        //arrange
        var clientId = "my-client-id";
        var clientSecret = "my-client-secret";
        var assertion = "jwt-assertion";
        var authMethod = "client_secret_post";

        //act
        var client = new OAuth2AuthenticationClientDefinitionBuilder()
            .WithId(clientId)
            .WithSecret(clientSecret)
            .WithAssertion(assertion)
            .WithAuthenticationMethod(authMethod)
            .Build();

        //assert
        client.Id.Should().Be(clientId);
        client.Secret.Should().Be(clientSecret);
        client.Assertion.Should().Be(assertion);
        client.Authentication.Should().Be(authMethod);
    }

    [Fact]
    public void Build_Should_Create_Client_With_Minimal_Properties()
    {
        //arrange
        var clientId = "minimal-client";

        //act
        var client = new OAuth2AuthenticationClientDefinitionBuilder()
            .WithId(clientId)
            .Build();

        //assert
        client.Id.Should().Be(clientId);
        client.Secret.Should().BeNull();
    }

}
