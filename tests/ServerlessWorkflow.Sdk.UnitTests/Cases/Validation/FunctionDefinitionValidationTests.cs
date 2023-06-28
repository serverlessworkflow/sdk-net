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

public class FunctionDefinitionValidationTests
{

    [Fact]
    public void Validate_Function_WithAutentication_ShouldWork()
    {
        //arrange
        var function = new FunctionDefinition()
        {
            Name = "Fake",
            Operation = "http://fake.com/fake#fake",
            AuthRef = "fake"
        };
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .AddBasicAuthentication("fake", auth => auth.LoadFromSecret("fake"))
            .StartsWith("fake", flow =>
                flow.Execute(action =>
                    action.Invoke(function)))
            .End()
            .Build();
       

        //act
        var result = new FunctionDefinitionValidator(workflow).Validate(function);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .BeNullOrEmpty();
    }

    [Fact]
    public void Validate_Function_NoAuthentication_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => 
                flow.Execute(action => 
                    action.Invoke(function => 
                        function.WithName("fake")
                            .OfType(FunctionType.Rest)
                            .ForOperation(new Uri("http://fake.com/fake#fake"))
                            .UseAuthentication("basic"))))
            .End()
            .Build();
        var function = new FunctionDefinition()
        {
            Name = "Fake",
            Operation = "http://fake.com/fake#fake",
            AuthRef = "fake"
        };

        //act
        var result = new FunctionDefinitionValidator(workflow).Validate(function);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(e => e.PropertyName == nameof(FunctionDefinition.AuthRef));
    }

}
