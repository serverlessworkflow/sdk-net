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

public class ListenerDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Listener_With_One_Event()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.example.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));

        //act
        var result = builder.Build();

        //assert
        result.To.One.Should().NotBeNull();
        result.To.One.With?[typeKey]?.GetValue<string>().Should().Be(typeValue);
    }

    [Fact]
    public void Build_Should_Create_Listener_With_Read_Mode()
    {
        //arrange
        var readMode = EventReadMode.Envelope;
        var typeKey = "type";
        var typeValue = "com.test";
        var builder = new ListenerDefinitionBuilder();
        builder.One().With(typeKey, JsonValue.Create(typeValue));
        builder.Read(readMode);

        //act
        var result = builder.Build();

        //assert
        result.Read.Should().Be(readMode);
        result.To.One.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Target()
    {
        //arrange
        var builder = new ListenerDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
