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

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class SwitchStateValidationTests
{

    public SwitchStateValidationTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddServerlessWorkflow();
        this.ServiceProvider = services.BuildServiceProvider();
        this.WorkflowDefinitionValidator = this.ServiceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
    }

    protected IServiceProvider ServiceProvider { get; }

    protected IValidator<WorkflowDefinition> WorkflowDefinitionValidator { get; }

    [Fact]
    public void Validate_SwitchState_NoDataOrEventConditions_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Switch())
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}"))
            .And.Contain(e => e.ErrorCode.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}"));
    }

    [Fact]
    public void Validate_SwitchState_NoDefaultCondition_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Switch())
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DefaultCondition)}"));
    }

}
