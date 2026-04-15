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

public class TimeoutDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Timeout_From_Duration()
    {
        //arrange
        var duration = Duration.FromSeconds(30);

        //act
        var timeout = new TimeoutDefinitionBuilder()
            .After(duration)
            .Build();

        //assert
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Timeout_From_String()
    {
        //arrange
        var durationString = "PT30S";

        //act
        var timeout = new TimeoutDefinitionBuilder()
            .After(durationString)
            .Build();

        //assert
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_After_Missing()
    {
        //arrange
        var builder = new TimeoutDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
