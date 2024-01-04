// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

using ServerlessWorkflow.Sdk.Services.IO;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.IO;

public class WorkflowReaderTests
{

    protected IWorkflowReader Reader { get; } = WorkflowReader.Create();

    [Fact]
    public async Task Read_Yaml_ShouldWork()
    {
        //arrange
        var yaml = File.ReadAllText(Path.Combine("Resources", "workflows", "operation.yaml"));

        //act
        var parsedWorkflow = await this.Reader.ParseAsync(yaml);

        //assert
        parsedWorkflow
            .Should()
            .NotBeNull();
        parsedWorkflow.Events
            .Should()
            .NotBeEmpty();
        parsedWorkflow.Functions
            .Should()
            .NotBeEmpty();
        parsedWorkflow.States
            .Should()
            .NotBeEmpty();
        parsedWorkflow.Metadata
            .Should()
            .NotBeNull();
        parsedWorkflow.Metadata!["podSize"]
            .Should()
            .Be("small");
    }

    [Fact]
    public async Task Read_Json_ShouldWork()
    {
        //arrange
        var yaml = File.ReadAllText(Path.Combine("Resources", "workflows", "operation.json"));

        //act
        var parsedWorkflow = await this.Reader.ParseAsync(yaml);

        //assert
        parsedWorkflow
            .Should()
            .NotBeNull();
        parsedWorkflow.Events
            .Should()
            .NotBeEmpty();
        parsedWorkflow.Functions
            .Should()
            .NotBeEmpty();
        parsedWorkflow.States
            .Should()
            .NotBeEmpty();
        parsedWorkflow.Metadata
            .Should()
            .NotBeNull();
        parsedWorkflow.Metadata!["podSize"]
            .Should()
            .Be("small");
    }

    [Fact]
    public async Task Read_Yaml_ExternalDefinitions_ShouldWork()
    {
        //arrange
        var yaml = File.ReadAllText(Path.Combine("Resources", "workflows", "externalref.yaml"));

        //act
        var workflow = await this.Reader.ParseAsync(yaml);

        //assert
        workflow
            .Should()
            .NotBeNull();
        workflow.ConstantsUri
            .Should()
            .NotBeNull();
        workflow.SecretsUri
            .Should()
            .NotBeNull();
        workflow.DataInputSchemaUri
            .Should()
            .NotBeNull();
        workflow.EventsUri
            .Should()
            .NotBeNull();
        workflow.FunctionsUri
            .Should()
            .NotBeNull();
        workflow.RetriesUri
            .Should()
            .NotBeNull();
    }

    [Fact]
    public async Task Read_Json_ExternalDefinitions_ShouldWork()
    {
        //arrange
        var yaml = File.ReadAllText(Path.Combine("Resources", "workflows", "externalref.json"));

        //act
        var workflow = await this.Reader.ParseAsync(yaml, new WorkflowReaderOptions() { LoadExternalDefinitions = true });

        //assert
        workflow
            .Should()
            .NotBeNull();
        workflow.Constants
            .Should()
            .NotBeNull();
        workflow.Secrets
            .Should()
            .NotBeEmpty();
        workflow.DataInputSchema
            .Should()
            .NotBeNull();
        workflow.Events
            .Should()
            .NotBeEmpty();
        workflow.Functions
            .Should()
            .NotBeEmpty();
        workflow.Retries
            .Should()
            .NotBeEmpty();
    }

}
