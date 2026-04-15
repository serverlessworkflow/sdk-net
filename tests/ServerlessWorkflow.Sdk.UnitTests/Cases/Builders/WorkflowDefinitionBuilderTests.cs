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

public class WorkflowDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Minimal_Workflow()
    {
        //arrange
        var workflowName = "test-workflow";
        var version = "1.0.0";
        var taskName = "greet";
        var key = "message";
        var value = "hello";
        var expectedTaskCount = 1;

        //act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName(workflowName)
            .WithVersion(version)
            .Do(taskName, task => task.Set(key, value))
            .Build();

        //assert
        workflow.Should().NotBeNull();
        workflow.Document.Name.Should().Be(workflowName);
        workflow.Document.Version.Should().Be(version);
        workflow.Document.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
        workflow.Do.Should().HaveCount(expectedTaskCount);
    }

    [Fact]
    public void Build_Should_Set_All_Document_Properties()
    {
        //arrange
        var dsl = "1.0.0";
        var ns = "my-namespace";
        var workflowName = "my-workflow";
        var version = "2.0.0";
        var title = "My Workflow";
        var summary = "A test workflow";
        var tagKey = "env";
        var tagValue = "test";
        var taskName = "step1";
        var key = "k";
        var value = "v";

        //act
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(dsl)
            .WithNamespace(ns)
            .WithName(workflowName)
            .WithVersion(version)
            .WithTitle(title)
            .WithSummary(summary)
            .WithTag(tagKey, tagValue)
            .Do(taskName, task => task.Set(key, value))
            .Build();

        //assert
        workflow.Document.Dsl.Should().Be(dsl);
        workflow.Document.Namespace.Should().Be(ns);
        workflow.Document.Name.Should().Be(workflowName);
        workflow.Document.Version.Should().Be(version);
        workflow.Document.Title.Should().Be(title);
        workflow.Document.Summary.Should().Be(summary);
        workflow.Document.Tags.Should().ContainKey(tagKey);
    }

    [Fact]
    public void Build_Should_Throw_When_Name_Missing()
    {
        //arrange
        var version = "1.0.0";
        var taskName = "step";
        var key = "k";
        var value = "v";
        var builder = new WorkflowDefinitionBuilder()
            .WithVersion(version)
            .Do(taskName, task => task.Set(key, value));

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Version_Missing()
    {
        //arrange
        var workflowName = "test";
        var taskName = "step";
        var key = "k";
        var value = "v";
        var builder = new WorkflowDefinitionBuilder()
            .WithName(workflowName)
            .Do(taskName, task => task.Set(key, value));

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Tasks()
    {
        //arrange
        var workflowName = "test";
        var version = "1.0.0";
        var builder = new WorkflowDefinitionBuilder()
            .WithName(workflowName)
            .WithVersion(version);

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void WithVersion_Should_Throw_For_Invalid_SemVer()
    {
        //arrange
        var invalidVersion = "not-semver";

        //act
        var act = () => new WorkflowDefinitionBuilder().WithVersion(invalidVersion);

        //assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void WithName_Should_Throw_For_Invalid_Name()
    {
        //arrange
        var invalidName = "INVALID NAME!";

        //act
        var act = () => new WorkflowDefinitionBuilder().WithName(invalidName);

        //assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Build_Should_Configure_Timeout()
    {
        //arrange
        var workflowName = "test";
        var version = "1.0.0";
        var timeoutDuration = Duration.FromSeconds(30);
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName(workflowName)
            .WithVersion(version)
            .WithTimeout(timeout => timeout.After(timeoutDuration))
            .Do(taskName, task => task.Set(key, value))
            .Build();

        //assert
        workflow.Timeout.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Configure_Components()
    {
        //arrange
        var workflowName = "test";
        var version = "1.0.0";
        var secret1 = "my-secret";
        var secret2 = "secret1";
        var secret3 = "secret2";
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var workflow = new WorkflowDefinitionBuilder()
            .WithName(workflowName)
            .WithVersion(version)
            .UseSecret(secret1)
            .UseSecret(secret2)
            .UseSecret(secret3)
            .Do(taskName, task => task.Set(key, value))
            .Build();

        //assert
        workflow.Use.Should().NotBeNull();
        workflow.Use!.Secrets.Should().Contain(secret1);
        workflow.Use.Secrets.Should().Contain(secret2);
    }

}
