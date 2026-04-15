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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ComponentDefinitionTests
{

    [Fact]
    public void TaskDefinition_Should_Be_ComponentDefinition()
    {
        //arrange & act
        var definition = new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } };

        //assert
        definition.Should().BeAssignableTo<ComponentDefinition>();
    }

    [Fact]
    public void ErrorDefinition_Should_Be_ReferenceableComponentDefinition()
    {
        //arrange
        var type = "https://errors.com/not-found";
        var title = "Not Found";
        var status = "404";

        //act
        var definition = new ErrorDefinition { Type = type, Title = title, Status = status };

        //assert
        definition.Should().BeAssignableTo<ReferenceableComponentDefinition>();
        definition.Should().BeAssignableTo<ComponentDefinition>();
    }

    [Fact]
    public void ReferenceableComponentDefinition_Should_Support_Ref()
    {
        //arrange
        var refUri = new Uri("https://schemas.example.com/error.json");

        //act
        var definition = new ErrorDefinition { Type = "t", Title = "t", Status = "400", Ref = refUri };

        //assert
        definition.Ref.Should().Be(refUri);
    }

}
