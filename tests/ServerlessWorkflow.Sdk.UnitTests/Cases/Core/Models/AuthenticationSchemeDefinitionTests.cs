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

public class AuthenticationSchemeDefinitionTests
{

    [Fact]
    public void Basic_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new BasicAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Basic);
    }

    [Fact]
    public void Bearer_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new BearerAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Certificate_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new CertificateAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Certificate);
    }

    [Fact]
    public void Digest_Should_Have_Correct_Scheme()
    {
        //arrange & act
        var scheme = new DigestAuthenticationSchemeDefinition();

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.Digest);
    }

    [Fact]
    public void Use_Property_Should_Be_Settable()
    {
        //arrange
        var secret = "my-secret";

        //act
        var scheme = new BasicAuthenticationSchemeDefinition { Use = secret };

        //assert
        scheme.Use.Should().Be(secret);
    }

}
