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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ProcessDefinitionTests
{

    [Fact]
    public void ContainerProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var image = "alpine:latest";

        //act
        var definition = new ContainerProcessDefinition { Image = image };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Image.Should().Be(image);
    }

    [Fact]
    public void ShellProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var command = "echo hello";

        //act
        var definition = new ShellProcessDefinition { Command = command };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Command.Should().Be(command);
    }

    [Fact]
    public void ScriptProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var language = "javascript";
        var code = "console.log('hello')";

        //act
        var definition = new ScriptProcessDefinition { Language = language, Code = code };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Language.Should().Be(language);
        definition.Code.Should().Be(code);
    }

    [Fact]
    public void WorkflowProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var ns = "default";
        var name = "sub-workflow";
        var version = "1.0.0";

        //act
        var definition = new WorkflowProcessDefinition { Namespace = ns, Name = name, Version = version };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Namespace.Should().Be(ns);
        definition.Name.Should().Be(name);
        definition.Version.Should().Be(version);
    }

}
