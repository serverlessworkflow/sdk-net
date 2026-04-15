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

public class CallTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Function_And_Arguments()
    {
        //arrange
        var functionName = "myFunction";
        var argName = "arg1";
        var argValue = "value1";

        //act
        var task = new CallTaskDefinitionBuilder()
            .Function(functionName)
            .With(argName, JsonValue.Create(argValue))
            .Build();

        //assert
        task.Call.Should().Be(functionName);
        task.With![argName]!.GetValue<string>().Should().Be(argValue);
    }

    [Fact]
    public void Build_Should_Accept_Function_Via_Constructor()
    {
        //arrange
        var functionName = "presetFunc";

        //act
        var task = new CallTaskDefinitionBuilder(functionName).Build();

        //assert
        task.Call.Should().Be(functionName);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Arguments()
    {
        //arrange
        var functionName = "fn";
        var key = "key";
        var value = "val";
        var args = new JsonObject { [key] = value };

        //act
        var task = new CallTaskDefinitionBuilder()
            .Function(functionName)
            .With(args)
            .Build();

        //assert
        task.With![key]!.GetValue<string>().Should().Be(value);
    }

    [Fact]
    public void Build_Should_Throw_When_Function_Missing()
    {
        //arrange
        var builder = new CallTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
