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

public class SchemaDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Schema_With_Format()
    {
        //arrange
        var format = "json";

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithFormat(format)
            .Build();

        //assert
        schema.Format.Should().Be(format);
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Document()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "object";
        var document = new JsonObject { [typeKey] = typeValue };

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithDocument(document)
            .Build();

        //assert
        schema.Document.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Resource()
    {
        //arrange
        var uri = new Uri("https://schemas.example.com/schema.json");

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithResource(r => r.WithEndpoint(e => e.WithUri(uri)))
            .Build();

        //assert
        schema.Resource.Should().NotBeNull();
    }

}
