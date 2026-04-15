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

public class BearerAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Token()
    {
        //arrange
        var token = "jwt-token-value";

        //act
        var scheme = new BearerAuthenticationSchemeDefinitionBuilder()
            .WithToken(token)
            .Build();

        //assert
        scheme.Token.Should().Be(token);
        scheme.Scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Build_Should_Set_Secret_Reference_Along_With_Token()
    {
        //arrange
        var secret = "my-bearer-secret";
        var token = "jwt-token";
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);

        //act
        var scheme = builder
            .WithToken(token)
            .Build();

        //assert
        scheme.Use.Should().Be(secret);
        scheme.Token.Should().Be(token);
    }

    [Fact]
    public void Build_Should_Throw_When_Token_Missing()
    {
        //arrange
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
