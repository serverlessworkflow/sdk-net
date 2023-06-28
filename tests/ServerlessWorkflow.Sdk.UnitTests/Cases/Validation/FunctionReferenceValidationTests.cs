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

public class FunctionReferenceValidationTests
{

    [Fact]
    public void Validate_FunctionReference_NameNotSet_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Inject(new { }))
            .End()
            .Build();
        var functionRef = new FunctionReference();

        //act
        var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(FunctionReference.RefName));
    }

    [Fact]
    public void Validate_FunctionReference_FunctionNotFound_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
           .StartsWith("fake", flow => flow.Callback())
           .End()
           .Build();
        var functionRef = new FunctionReference() { RefName = "fake" };

        //act
        var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

        //assert
        result.Should()
           .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(FunctionReference.RefName));
    }

    [Fact]
    public void Validate_FunctionReference_GraphQL_SelectionSetEmpty_ShouldFail()
    {
        //arrange
        var functionName = "fake";
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .AddFunction(function => function
                .WithName(functionName)
                .OfType(FunctionType.GraphQL))
            .StartsWith("fake", flow => flow
                .Execute(action => action.Invoke(functionName)))
            .End()
            .Build();
        var functionRef = new FunctionReference() { RefName = functionName };

        //act
        var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(FunctionReference.SelectionSet));
    }

    [Fact]
    public void Validate_FunctionReference_SelectionSetNotEmpty_ShouldFail()
    {
        //arrange
        var functionName = "fake";
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .AddFunction(function => function
                .WithName(functionName)
                .OfType(FunctionType.Rest))
            .StartsWith("fake", flow => flow
                .Execute(action => action.Invoke(functionName)))
            .End()
            .Build();
        var functionRef = new FunctionReference() { RefName = functionName, SelectionSet = "{ id, name }" };

        //act
        var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(FunctionReference.SelectionSet));
    }

}
