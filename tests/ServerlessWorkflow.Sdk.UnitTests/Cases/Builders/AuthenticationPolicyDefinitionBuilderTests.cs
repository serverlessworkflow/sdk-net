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

public class AuthenticationPolicyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Basic_Authentication_Policy()
    {
        //arrange
        var username = "admin";
        var password = "s3cret";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Basic().WithUsername(username).WithPassword(password);

        //act
        var policy = builder.Build();

        //assert
        policy.Basic.Should().NotBeNull();
        policy.Basic!.Username.Should().Be(username);
        policy.Basic!.Password.Should().Be(password);
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Bearer_Authentication_Policy()
    {
        //arrange
        var token = "eyJhbGciOi...";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Bearer().WithToken(token);

        //act
        var policy = builder.Build();

        //assert
        policy.Bearer.Should().NotBeNull();
        policy.Bearer!.Token.Should().Be(token);
        policy.Basic.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Digest_Authentication_Policy()
    {
        //arrange
        var username = "admin";
        var password = "digest-pass";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Digest().WithUsername(username).WithPassword(password);

        //act
        var policy = builder.Build();

        //assert
        policy.Basic.Should().BeNull();
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Scheme_Configured()
    {
        //arrange
        var builder = new AuthenticationPolicyDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
