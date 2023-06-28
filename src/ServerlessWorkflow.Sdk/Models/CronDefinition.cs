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
/// Represents a CRON expression definition
/// </summary>
[DataContract]
public class CronDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the repeating interval (cron expression) describing when the workflow instance should be created
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "expression", IsRequired = true), JsonPropertyName("expression"), YamlMember(Alias = "expression")]
    public virtual string Expression { get; set; } = null!;

    /// <summary>
    /// Gets/sets the date and time when the cron expression invocation is no longer valid
    /// </summary>
    [DataMember(Order = 2, Name = "validUntil", IsRequired = true), JsonPropertyName("validUntil"), YamlMember(Alias = "validUntil")]
    public virtual DateTime? ValidUntil { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}
