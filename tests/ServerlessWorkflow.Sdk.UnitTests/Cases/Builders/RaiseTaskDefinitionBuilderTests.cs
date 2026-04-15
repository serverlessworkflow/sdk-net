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

public class RaiseTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Error_Via_Builder()
    {
        //arrange
        var errorType = "https://errors.com/not-found";
        var errorTitle = "Not Found";
        var errorStatus = "404";

        //act
        var task = new RaiseTaskDefinitionBuilder()
            .Error(e => e.WithType(errorType).WithTitle(errorTitle).WithStatus(errorStatus))
            .Build();

        //assert
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorType);
        error!.Title.Should().Be(errorTitle);
        error!.Status.Should().Be(errorStatus);
    }

    [Fact]
    public void Build_Should_Set_Error_Via_Definition()
    {
        //arrange
        var errorType = "https://err.com/t";
        var errorTitle = "T";
        var errorStatus = "500";
        var errorDef = new ErrorDefinition { Type = errorType, Title = errorTitle, Status = errorStatus };

        //act
        var task = new RaiseTaskDefinitionBuilder()
            .Error(errorDef)
            .Build();

        //assert
        var error = task.Raise.Error.Match<ErrorDefinition?>(e => e, _ => null);
        error.Should().NotBeNull();
        error!.Type.Should().Be(errorType);
    }

    [Fact]
    public void Build_Should_Throw_When_Error_Missing()
    {
        //arrange
        var builder = new RaiseTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
