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

using ServerlessWorkflow.Sdk.Builders;
using ServerlessWorkflow.Sdk.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class WorkflowDefinitionValidationTests
{

    [Fact]
    public async Task Validate_Workflow_With_Task_Flowing_To_Undefined_Task_Should_Fail()
    {
        //arrange
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(DslVersion.V1Alpha2)
            .WithNamespace("fake-namespace")
            .WithName("fake-workflow")
            .WithVersion("0.1.0-fake")
            .Do("fake-task-1", task => task
                .Set("foo", "bar")
                .Then("undefined"))
            .Build();
        var validator = WorkflowDefinitionValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow);

        //assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_Workflow_With_Undefined_Timeout_Should_Fail()
    {
        //arrange
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(DslVersion.V1Alpha2)
            .WithNamespace("fake-namespace")
            .WithName("fake-workflow")
            .WithVersion("0.1.0-fake")
            .WithTimeout("undefined")
            .Do("fake-task-1", task => task
                .Set("foo", "bar"))
            .Build();
        var validator = WorkflowDefinitionValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow);

        //assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_Workflow_With_Task_With_Undefined_Timeout_Should_Fail()
    {
        //arrange
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(DslVersion.V1Alpha2)
            .WithNamespace("fake-namespace")
            .WithName("fake-workflow")
            .WithVersion("0.1.0-fake")
            .Do("fake-task-1", task => task
                .Set("foo", "bar")
                .WithTimeout("undefined"))
            .Build();
        var validator = WorkflowDefinitionValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow);

        //assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_Workflow_With_Call_To_Undefined_Function_Should_Fail()
    {
        //arrange
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(DslVersion.V1Alpha2)
            .WithNamespace("fake-namespace")
            .WithName("fake-workflow")
            .WithVersion("0.1.0-fake")
            .Do("fake-task-1", task => task
                .Call("undefined"))
            .Build();
        var validator = WorkflowDefinitionValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow);

        //assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_Workflow_With_Switch_Flowing_To_Undefined_Task_Should_Fail()
    {
        //arrange
        var workflow = new WorkflowDefinitionBuilder()
            .UseDsl(DslVersion.V1Alpha2)
            .WithNamespace("fake-namespace")
            .WithName("fake-workflow")
            .WithVersion("0.1.0-fake")
            .Do("fake-task-1", task => task
                .Switch()
                    .Case("fake-case-1", @case =>
                        @case.Then("undefined")))
            .Build();
        var validator = WorkflowDefinitionValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow);

        //assert
        result.IsValid.Should().BeFalse();
    }

}
