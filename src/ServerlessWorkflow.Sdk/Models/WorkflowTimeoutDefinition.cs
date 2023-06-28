// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the object used to configure a workflow definition's <see href="https://github.com/serverlessworkflow/specification/blob/main/specification.md#Workflow-Timeouts">timeout</see>
/// </summary>
[DataContract]
public class WorkflowTimeoutDefinition
{

    /// <summary>
    /// Gets/sets the workflow definition's execution timeout
    /// </summary>
    [DataMember(Order = 1, Name = "workflowExecTimeout"), JsonPropertyName("workflowExecTimeout"), YamlMember(Alias = "workflowExecTimeout")]
    public virtual OneOf<WorkflowExecutionTimeoutDefinition, string>? WorkflowExecTimeout { get; set; }

    /// <summary>
    /// Gets/sets the duration after which to timeout states by default
    /// </summary>
    [DataMember(Order = 2, Name = "stateExecTimeout"), JsonPropertyName("stateExecTimeout"), YamlMember(Alias = "stateExecTimeout")]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public TimeSpan? StateExecutionTimeout { get; set; }

    /// <summary>
    /// Gets/sets the duration after which to timeout actions by default
    /// </summary>
    [DataMember(Order = 3, Name = "actionExecTimeout"), JsonPropertyName("actionExecTimeout"), YamlMember(Alias = "actionExecTimeout")]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))] 
    public TimeSpan? ActionExecTimeout { get; set; }

    /// <summary>
    /// Gets/sets the duration after which to timeout branches by default
    /// </summary>
    [DataMember(Order = 4, Name = "branchExecTimeout"), JsonPropertyName("branchExecTimeout"), YamlMember(Alias = "branchExecTimeout")]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))] 
    public TimeSpan? BranchExecutionTimeout { get; set; }

    /// <summary>
    /// Gets/sets the duration after which to timeout events by default
    /// </summary>
    [DataMember(Order = 5, Name = "eventTimeout"), JsonPropertyName("eventTimeout"), YamlMember(Alias = "eventTimeout")]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))] 
    public TimeSpan? EventTimeout { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 6, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}