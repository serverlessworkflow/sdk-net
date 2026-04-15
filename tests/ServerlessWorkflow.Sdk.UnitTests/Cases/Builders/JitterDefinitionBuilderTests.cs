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

public class JitterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Jitter_With_Range()
    {
        //arrange
        var from = Duration.FromMilliseconds(100);
        var to = Duration.FromMilliseconds(500);

        //act
        var definition = new JitterDefinitionBuilder()
            .From(from)
            .To(to)
            .Build();

        //assert
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

    [Fact]
    public void Build_Should_Create_Jitter_From_Constructor()
    {
        //arrange
        var from = Duration.FromMilliseconds(50);
        var to = Duration.FromMilliseconds(200);

        //act
        var definition = new JitterDefinitionBuilder(from, to).Build();

        //assert
        definition.From.Should().Be(from);
        definition.To.Should().Be(to);
    }

}
