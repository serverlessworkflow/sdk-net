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

public class RetryPolicyLimitDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Limit_With_Attempt()
    {
        //arrange
        uint maxAttempts = 5;
        var builder = new RetryPolicyLimitDefinitionBuilder();
        builder.Attempt().Count(maxAttempts);

        //act
        var limit = builder.Build();

        //assert
        limit.Attempt.Should().NotBeNull();
        limit.Attempt!.Count.Should().Be(maxAttempts);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Duration()
    {
        //arrange
        var maxDuration = Duration.FromMinutes(5);

        //act
        var limit = new RetryPolicyLimitDefinitionBuilder()
            .Duration(maxDuration)
            .Build();

        //assert
        limit.Duration.Should().Be(maxDuration);
    }

}
