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

public class RetryPolicyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Policy_With_When_Expression()
    {
        //arrange
        var whenExpr = "${ .retryable }";

        //act
        var policy = new RetryPolicyDefinitionBuilder()
            .When(whenExpr)
            .Build();

        //assert
        policy.When.Should().Be(whenExpr);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_ExceptWhen()
    {
        //arrange
        var exceptWhenExpr = "${ .fatal }";

        //act
        var policy = new RetryPolicyDefinitionBuilder()
            .ExceptWhen(exceptWhenExpr)
            .Build();

        //assert
        policy.ExceptWhen.Should().Be(exceptWhenExpr);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Delay()
    {
        //arrange
        var delay = Duration.FromSeconds(5);

        //act
        var policy = new RetryPolicyDefinitionBuilder()
            .Delay(delay)
            .Build();

        //assert
        policy.Delay.Should().Be(delay);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Backoff()
    {
        //arrange
        var builder = new RetryPolicyDefinitionBuilder();

        //act
        var policy = builder
            .Backoff(b => b.Exponential())
            .Build();

        //assert
        policy.Backoff.Should().NotBeNull();
        policy.Backoff!.Exponential.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Jitter()
    {
        //arrange
        var from = Duration.FromMilliseconds(100);
        var to = Duration.FromMilliseconds(500);

        //act
        var policy = new RetryPolicyDefinitionBuilder()
            .Jitter(j => j.From(from).To(to))
            .Build();

        //assert
        policy.Jitter.Should().NotBeNull();
        policy.Jitter!.From.Should().Be(from);
        policy.Jitter!.To.Should().Be(to);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Limit()
    {
        //arrange
        uint maxAttempts = 3;

        //act
        var policy = new RetryPolicyDefinitionBuilder()
            .Limit(l => l.Attempt().Count(maxAttempts))
            .Build();

        //assert
        policy.Limit.Should().NotBeNull();
        policy.Limit!.Attempt.Should().NotBeNull();
        policy.Limit!.Attempt!.Count.Should().Be(maxAttempts);
    }

}
