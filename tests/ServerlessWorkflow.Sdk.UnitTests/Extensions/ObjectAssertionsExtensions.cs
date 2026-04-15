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

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.UnitTests;

internal static class ObjectAssertionsExtensions
{

    public static void BeJsonEquivalentTo<T>(this ObjectAssertions should, T expected)
    {
        should.BeEquivalentTo(expected, opts => opts
            .Using<JsonElement>(ctx => 
                ctx.Subject.ToJsonString().Should().Be(ctx.Expectation.ToJsonString()))
                .WhenTypeIs<JsonElement>()
            .Using<JsonNode>(ctx =>
                ctx.Subject?.ToJsonString().Should().Be(ctx.Expectation?.ToJsonString()))
                .WhenTypeIs<JsonNode>()
            .Using<JsonObject>(ctx =>
                ctx.Subject?.ToJsonString().Should().Be(ctx.Expectation?.ToJsonString()))
                .WhenTypeIs<JsonObject>()
            .Using<JsonPatch>(ctx =>
                ctx.Subject?.ToJsonDocument().RootElement.ToJsonString().Should().Be(ctx.Expectation?.ToJsonDocument().RootElement.ToJsonString()))
                .WhenTypeIs<JsonPatch>());
    }

}
