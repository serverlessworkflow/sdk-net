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

public class ShellProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Shell_With_All_Properties()
    {
        //arrange
        var command = "echo hello";
        var argument = "--verbose";
        var envKey = "PATH";
        var envValue = "/usr/bin";

        //act
        var shell = new ShellProcessDefinitionBuilder()
            .WithCommand(command)
            .WithArgument(argument)
            .WithEnvironment(envKey, envValue)
            .Build();

        //assert
        shell.Command.Should().Be(command);
        shell.Arguments.Should().Contain(argument);
        shell.Environment.Should().ContainKey(envKey);
    }

    [Fact]
    public void Build_Should_Accept_Bulk_Arguments_And_Environment()
    {
        //arrange
        var command = "ls";
        var arg1 = "-l";
        var arg2 = "-a";
        var envKey = "HOME";
        var envValue = "/root";
        var expectedArgCount = 2;

        //act
        var shell = new ShellProcessDefinitionBuilder()
            .WithCommand(command)
            .WithArguments([arg1, arg2])
            .WithEnvironment(new Dictionary<string, string> { [envKey] = envValue })
            .Build();

        //assert
        shell.Command.Should().Be(command);
        shell.Arguments.Should().HaveCount(expectedArgCount);
        shell.Arguments.Should().Contain(arg1);
        shell.Arguments.Should().Contain(arg2);
        shell.Environment![envKey].Should().Be(envValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Command_Missing()
    {
        //arrange
        var builder = new ShellProcessDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
