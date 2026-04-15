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

public class DigestAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Username_And_Password()
    {
        //arrange
        var username = "admin";
        var password = "secret";

        //act
        var scheme = new DigestAuthenticationSchemeDefinitionBuilder()
            .WithUsername(username)
            .WithPassword(password)
            .Build();

        //assert
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scheme.Should().Be(AuthenticationScheme.Digest);
    }

}
