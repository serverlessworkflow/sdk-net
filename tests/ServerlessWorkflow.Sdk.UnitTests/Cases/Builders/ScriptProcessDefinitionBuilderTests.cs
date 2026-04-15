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

public class ScriptProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Script_With_Language_And_Code()
    {
        //arrange
        var language = "javascript";
        var code = "console.log('hello')";

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithCode(code)
            .Build();

        //assert
        script.Language.Should().Be(language);
        script.Code.Should().Be(code);
    }

    [Fact]
    public void Build_Should_Create_Script_With_Source()
    {
        //arrange
        var language = "python";
        var sourceUri = new Uri("https://scripts.example.com/run.py");

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithSource(s => s.WithEndpoint(e => e.WithUri(sourceUri)))
            .Build();

        //assert
        script.Language.Should().Be(language);
        script.Source.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Script_With_Arguments_And_Environment()
    {
        //arrange
        var language = "bash";
        var code = "echo $MSG";
        var argName = "verbose";
        var argValue = "true";
        var envName = "MSG";
        var envValue = "hello";

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithCode(code)
            .WithArgument(argName, argValue)
            .WithEnvironment(envName, envValue)
            .Build();

        //assert
        script.Arguments.Should().ContainKey(argName);
        script.Environment.Should().ContainKey(envName);
    }

    [Fact]
    public void Build_Should_Throw_When_Language_Missing()
    {
        //arrange
        var code = "print('hi')";

        //act
        var act = () => new ScriptProcessDefinitionBuilder()
            .WithCode(code)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
