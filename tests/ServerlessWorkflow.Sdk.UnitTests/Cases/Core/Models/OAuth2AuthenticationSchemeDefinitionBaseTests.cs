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

public class OAuth2AuthenticationSchemeDefinitionBaseTests
{

    [Fact]
    public void Should_Set_All_Properties()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var username = "user";
        var password = "pass";
        var scope = "read";
        var audience = "api";
        var issuer = "https://issuer.example.com";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinition
        {
            Authority = authority,
            Grant = grantType,
            Username = username,
            Password = password,
            Scopes = [scope],
            Audiences = [audience],
            Issuers = [issuer]
        };

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scopes.Should().Contain(scope);
        scheme.Audiences.Should().Contain(audience);
        scheme.Issuers.Should().Contain(issuer);
        scheme.Scheme.Should().Be(AuthenticationScheme.OAuth2);
    }

    [Fact]
    public void OpenIDConnect_Should_Have_Correct_Scheme()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");

        //act
        var scheme = new OpenIDConnectSchemeDefinition { Authority = authority, Grant = "authorization_code" };

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.OpenIDConnect);
    }

}
