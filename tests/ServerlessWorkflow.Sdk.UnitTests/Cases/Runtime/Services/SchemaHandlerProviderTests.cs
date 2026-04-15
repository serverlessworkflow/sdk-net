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

using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class SchemaHandlerProviderTests
{

    [Fact]
    public void GetHandler_Should_Return_Handler_That_Supports_Format()
    {
        //arrange
        var format = "json";
        var mockHandler = new Mock<ISchemaHandler>();
        mockHandler.Setup(h => h.Supports(format)).Returns(true);
        var provider = new SchemaHandlerProvider([mockHandler.Object]);

        //act
        var handler = provider.GetHandler(format);

        //assert
        handler.Should().Be(mockHandler.Object);
    }

    [Fact]
    public void GetHandler_Should_Return_Null_When_No_Handler_Supports_Format()
    {
        //arrange
        var format = "unsupported";
        var mockHandler = new Mock<ISchemaHandler>();
        mockHandler.Setup(h => h.Supports(It.IsAny<string>())).Returns(false);
        var provider = new SchemaHandlerProvider([mockHandler.Object]);

        //act
        var handler = provider.GetHandler(format);

        //assert
        handler.Should().BeNull();
    }

    [Fact]
    public void GetHandler_Should_Return_First_Matching_Handler()
    {
        //arrange
        var format = "json";
        var handler1 = new Mock<ISchemaHandler>();
        handler1.Setup(h => h.Supports(format)).Returns(true);
        var handler2 = new Mock<ISchemaHandler>();
        handler2.Setup(h => h.Supports(format)).Returns(true);
        var provider = new SchemaHandlerProvider([handler1.Object, handler2.Object]);

        //act
        var result = provider.GetHandler(format);

        //assert
        result.Should().Be(handler1.Object);
    }

}
