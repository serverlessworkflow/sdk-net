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

using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CloudEventFactory
{
    internal static CloudEvent Create() => new()
    {
        Id = "ce-123",
        SpecVersion = CloudEvent.DefaultVersion,
        Time = new DateTimeOffset(2026, 4, 3, 12, 0, 0, TimeSpan.Zero),
        Source = new Uri("https://example.com/source"),
        Type = "com.example.test",
        Subject = "test-subject",
        DataContentType = "application/json",
        DataSchema = new Uri("https://example.com/schema"),
        Data = new JsonObject { ["key"] = "value" }
    };
}
