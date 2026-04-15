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

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TaskDescriptorFactory
{
    internal static TaskDescriptor Create() => new()
    {
        Id = "test-task",
        Name = "test",
        Definition = TaskDefinitionFactory.CreateSetTask(),
        Reference = JsonPointer.Parse("/test"),
        Input = new JsonObject()
        {
            ["key"] = "value"
        },
        Output = new JsonObject()
        {
            ["key"] = "value"
        },
        StartedAt = DateTimeDescriptorFactory.Create()
    };
}
