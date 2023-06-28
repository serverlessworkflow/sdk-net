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

using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class WorkflowValidationTests
{

    [Fact]
    public async Task Validate_WorkflowDefinition_Should_Work()
    {
        //arrange
        var workflow = WorkflowDefinitionFactory.Create();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_WorkflowDefinition_WithStateTypeExtensions_Should_Work()
    {
        //arrange
        var workflow = new WorkflowBuilder()
            .WithId("fake")
            .WithName("Fake Workflow")
            .WithDescription("Fake Workflow Description")
            .UseSpecVersion(ServerlessWorkflowSpecVersion.Latest)
            .WithVersion("1.0.0")
            .UseExtension("fake-extension", new($"file://{Path.Combine(AppContext.BaseDirectory, "Assets", "WorkflowExtensions", "condition-state-type.json")}"))
            .StartsWith("fake-state", flow => flow
                .Extension("condition")
                .WithExtensionProperty("if", new { condition = "${ true }", action = new { name = "fake", functionRef = new FunctionReference() { RefName = "fake-function" } } })
                .WithExtensionProperty("else", new { action = new { name = "fake", functionRef = new FunctionReference() { RefName = "fake-function" } } }))
            .End()
            .Build();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_WorkflowDefinition_WithFunctionTypeExtension_Should_Work()
    {
        //arrange
        var workflow = new WorkflowBuilder()
            .WithId("fake")
            .WithName("Fake Workflow")
            .WithDescription("Fake Workflow Description")
            .UseSpecVersion(ServerlessWorkflowSpecVersion.Latest)
            .WithVersion("1.0.0")
            .UseExtension("fake-extension", new($"file://{Path.Combine(AppContext.BaseDirectory, "Assets", "WorkflowExtensions", "greet-function-type.json")}"))
            .StartsWith("fake-state", flow => flow
                .Execute(action => action
                    .Invoke(function => function
                        .ForOperation("https://unittests.sdk-net.serverlessworkflow.io#fake-operation")
                        .OfType("greet")
                        .WithName("greet"))))
            .End()
            .Build();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

}
