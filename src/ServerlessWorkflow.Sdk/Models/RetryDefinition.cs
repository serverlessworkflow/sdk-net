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
/// Represents an object that defines workflow states retry policy strategy. This is an explicit definition and can be reused across multiple defined workflow state errors.
/// </summary>
[DataContract]
public class RetryDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the <see cref="RetryDefinition"/>'s name
    /// </summary>
    [Required]
    [DataMember(Order = 1, Name = "name", IsRequired = true), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 1)]
    public virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets delay between retry attempts
    /// </summary>
    [DataMember(Order = 2, Name = "delay"), JsonPropertyOrder(2), JsonPropertyName("delay"), YamlMember(Alias = "delay", Order = 2)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? Delay { get; set; }

    /// <summary>
    /// Gets/sets the maximum amount of retries allowed
    /// </summary>
    [DataMember(Order = 3, Name = "maxAttempts"), JsonPropertyOrder(3), JsonPropertyName("maxAttempts"), YamlMember(Alias = "maxAttempts", Order = 3)]
    public virtual uint? MaxAttempts { get; set; }

    /// <summary>
    /// Gets/sets the maximum delay between retries
    /// </summary>
    [DataMember(Order = 4, Name = "maxDelay"), JsonPropertyOrder(4), JsonPropertyName("maxDelay"), YamlMember(Alias = "maxDelay", Order = 4)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? MaxDelay { get; set; }

    /// <summary>
    /// Gets/sets the duration which will be added to the delay between successive retries
    /// </summary>
    [DataMember(Order = 5, Name = "increment"), JsonPropertyOrder(5), JsonPropertyName("increment"), YamlMember(Alias = "increment", Order = 5)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? Increment { get; set; }

    /// <summary>
    /// Gets/sets a value by which the delay is multiplied before each attempt. For example: "1.2" meaning that each successive delay is 20% longer than the previous delay. 
    /// For example, if delay is 'PT10S', then the delay between the first and second attempts will be 10 seconds, and the delay before the third attempt will be 12 seconds.
    /// </summary>
    [DataMember(Order = 6, Name = "multiplier"), JsonPropertyOrder(6), JsonPropertyName("multiplier"), YamlMember(Alias = "multiplier", Order = 6)]
    public virtual float? Multiplier { get; set; }

    /// <summary>
    /// Gets/sets the object that represents the <see cref="RetryDefinition"/>'s jitter.<para></para>
    /// If float type, maximum amount of random time added or subtracted from the delay between each retry relative to total delay (between 0.0 and 1.0).<para></para>
    /// If string type, absolute maximum amount of random time added or subtracted from the delay between each retry (ISO 8601 duration format)
    /// </summary>
    [DataMember(Order = 7, Name = "jitter"), JsonPropertyOrder(7), JsonPropertyName("jitter"), YamlMember(Alias = "jitter", Order = 7)]
    [JsonConverter(typeof(OneOfConverter<float?, string>))]
    public virtual OneOf<float?, string>? JitterValue { get; set; }

    /// <summary>
    /// Gets/sets the maximum amount of random time added or subtracted from the delay between each retry relative to total delay (between 0.0 and 1.0)
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual float? JitterMultiplier
    {
        get
        {
            return this.JitterValue?.T1Value;
        }
        set
        {
            if (value == null) this.JitterValue = null;
            else this.JitterValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the absolute maximum amount of random time added or subtracted from the delay between each retry
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual TimeSpan? JitterDuration
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this.JitterValue?.T2Value))
                return null;
            return Iso8601TimeSpan.Parse(this.JitterValue.T2Value);
        }
        set
        {
            if (value == null) this.JitterValue = null;
            else this.JitterValue = Iso8601TimeSpan.Format(value.Value);
        }
    }

    /// <inheritdoc/>
    [DataMember(Order = 8, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => this.Name;

}