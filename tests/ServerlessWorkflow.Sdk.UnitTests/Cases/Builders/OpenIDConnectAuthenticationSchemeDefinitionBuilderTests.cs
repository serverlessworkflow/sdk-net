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

public class OpenIDConnectAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";

        //act
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Issuers()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";
        var issuer = "https://oidc.example.com";

        //act
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithIssuers(issuer)
            .Build();

        //assert
        scheme.Issuers.Should().Contain(issuer);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        //arrange
        var grantType = "authorization_code";

        //act
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithGrantType(grantType)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");

        //act
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
