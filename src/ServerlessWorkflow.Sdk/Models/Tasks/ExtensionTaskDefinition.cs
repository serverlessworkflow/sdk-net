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

namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of an extension's task
/// </summary>
[Description("Represents the definition of an extension's task")]
[DataContract]
public sealed record ExtensionTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Extension;

    /// <summary>
    /// Gets/sets the task definition's extension data, if any
    /// </summary>
    [Description("The task definition's extension data, if any")]
    [DataMember(Order = 1, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, JsonElement>? ExtensionData { get; set; }

}