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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class AuthenticationResultTests
{

    [Fact]
    public void Should_Set_Scheme_And_Value()
    {
        //arrange
        var scheme = "Bearer";
        var value = "eyJhbGciOiJSUzI1NiJ9...";

        //act
        var result = new AuthenticationResult(scheme, value);

        //assert
        result.Scheme.Should().Be(scheme);
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Should_Implement_IAuthenticationResult()
    {
        //arrange
        var scheme = "Basic";
        var value = "dXNlcjpwYXNz";

        //act
        IAuthenticationResult result = new AuthenticationResult(scheme, value);

        //assert
        result.Scheme.Should().Be(scheme);
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Should_Support_Record_Equality()
    {
        //arrange
        var scheme = "Bearer";
        var value = "token123";
        var result1 = new AuthenticationResult(scheme, value);
        var result2 = new AuthenticationResult(scheme, value);

        //act & assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void Should_Support_Record_With_Expression()
    {
        //arrange
        var originalScheme = "Bearer";
        var originalValue = "old-token";
        var newValue = "new-token";
        var original = new AuthenticationResult(originalScheme, originalValue);

        //act
        var updated = original with { Value = newValue };

        //assert
        updated.Scheme.Should().Be(originalScheme);
        updated.Value.Should().Be(newValue);
        original.Value.Should().Be(originalValue);
    }

}
