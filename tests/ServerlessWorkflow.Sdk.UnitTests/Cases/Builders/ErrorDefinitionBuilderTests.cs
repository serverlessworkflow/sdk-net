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

public class ErrorDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Error_With_All_Properties()
    {
        //arrange
        var type = "https://errors.com/not-found";
        var title = "Not Found";
        var status = "404";
        var detail = "Resource not found";
        var instance = "/items/123";

        //act
        var error = new ErrorDefinitionBuilder()
            .WithType(type)
            .WithTitle(title)
            .WithStatus(status)
            .WithDetail(detail)
            .WithInstance(instance)
            .Build();

        //assert
        error.Type.Should().Be(type);
        error.Title.Should().Be(title);
        error.Status.Should().Be(status);
        error.Detail.Should().Be(detail);
        error.Instance.Should().Be(instance);
    }

    [Fact]
    public void Build_Should_Throw_When_Type_Missing()
    {
        //arrange
        var title = "t";
        var status = "400";

        //act
        var act = () => new ErrorDefinitionBuilder().WithTitle(title).WithStatus(status).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Title_Missing()
    {
        //arrange
        var type = "t";
        var status = "400";

        //act
        var act = () => new ErrorDefinitionBuilder().WithType(type).WithStatus(status).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Status_Missing()
    {
        //arrange
        var type = "t";
        var title = "t";

        //act
        var act = () => new ErrorDefinitionBuilder().WithType(type).WithTitle(title).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
