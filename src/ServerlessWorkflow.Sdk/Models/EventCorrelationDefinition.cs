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
/// Represents an object used to define the way to correlate a cloud event
/// </summary>
[DataContract]
public class EventCorrelationDefinition
    : IExtensible
{

    /// <summary>
    /// Initializes a new <see cref="EventCorrelationDefinition"/>
    /// </summary>
    public EventCorrelationDefinition() { }

    /// <summary>
    /// Initializes a new <see cref="EventCorrelationDefinition"/>
    /// </summary>
    /// <param name="attributeName">The name of the cloud event extension attribute to correlate events by</param>
    /// <param name="attributeValue">The value of the cloud event extension attribute to correlate events by</param>
    public EventCorrelationDefinition(string attributeName, string attributeValue)
    {
        if(string.IsNullOrWhiteSpace(attributeName)) throw new ArgumentNullException(nameof(attributeName));
        if (string.IsNullOrWhiteSpace(attributeValue)) throw new ArgumentNullException(nameof(attributeValue));
        this.ContextAttributeName = attributeName;
        this.ContextAttributeValue = attributeValue;
    }

    /// <summary>
    /// Gets/sets the name of the cloud event extension attribute to correlate events by
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "contextAttributeName", IsRequired = true), JsonPropertyName("contextAttributeName"), YamlMember(Alias = "contextAttributeName")]
    public virtual string ContextAttributeName { get; set; } = null!;

    /// <summary>
    /// Gets/sets the value of the cloud event extension attribute to correlate events by
    /// </summary>
    [DataMember(Order = 2, Name = "contextAttributeValue", IsRequired = true), JsonPropertyName("contextAttributeValue"), YamlMember(Alias = "contextAttributeValue")]
    public virtual string? ContextAttributeValue { get; set; }

    /// <summary>
    /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="EventCorrelationDefinition"/>'s extension properties
    /// </summary>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}