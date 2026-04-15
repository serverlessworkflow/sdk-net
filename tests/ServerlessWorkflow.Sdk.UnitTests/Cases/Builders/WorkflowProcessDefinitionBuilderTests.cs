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

public class WorkflowProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Workflow_Process_With_All_Properties()
    {
        //arrange
        var ns = "my-namespace";
        var name = "sub-workflow";
        var version = "1.0.0";
        var inputKey = "key";
        var inputValue = "value";
        var inputData = new JsonObject { [inputKey] = inputValue };

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .WithVersion(version)
            .WithInput(inputData)
            .Build();

        //assert
        process.Namespace.Should().Be(ns);
        process.Name.Should().Be(name);
        process.Version.Should().Be(version);
        process.Input![inputKey]!.GetValue<string>().Should().Be(inputValue);
    }

    [Fact]
    public void Build_Should_Use_Default_Namespace_When_Not_Set()
    {
        //arrange
        var name = "sub-workflow";
        var version = "1.0.0";

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithName(name)
            .WithVersion(version)
            .Build();

        //assert
        process.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
    }

    [Fact]
    public void Build_Should_Use_Default_Version_When_Not_Set()
    {
        //arrange
        var name = "sub-workflow";
        var ns = "my-namespace";
        var expectedDefaultVersion = "latest";

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .Build();

        //assert
        process.Version.Should().Be(expectedDefaultVersion);
    }

    [Fact]
    public void Build_Should_Throw_When_Name_Missing()
    {
        //arrange
        var ns = "ns";
        var version = "1.0.0";

        //act
        var act = () => new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithVersion(version)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
