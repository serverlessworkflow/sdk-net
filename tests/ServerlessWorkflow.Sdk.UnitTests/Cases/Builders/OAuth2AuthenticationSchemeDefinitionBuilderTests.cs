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

public class OAuth2AuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Client_Via_Builder()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var clientId = "my-client";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithClient(c => c.WithId(clientId))
            .Build();

        //assert
        scheme.Client.Should().NotBeNull();
        scheme.Client!.Id.Should().Be(clientId);
    }

    [Fact]
    public void Build_Should_Set_Scopes_And_Audiences()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var scope = "read";
        var audience = "api";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithScopes(scope)
            .WithAudiences(audience)
            .Build();

        //assert
        scheme.Scopes.Should().Contain(scope);
        scheme.Audiences.Should().Contain(audience);
    }

    [Fact]
    public void Build_Should_Set_Password_Grant_Credentials()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "password";
        var username = "user";
        var password = "pass";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithUsername(username)
            .WithPassword(password)
            .Build();

        //assert
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        //arrange
        var grantType = "client_credentials";

        //act
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithGrantType(grantType)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");

        //act
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
