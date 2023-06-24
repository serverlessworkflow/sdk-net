namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define the time/repeating intervals at which workflow instances can/should be started 
/// </summary>
[DataContract]
public class ScheduleDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets the time interval (ISO 8601 format) describing when workflow instances can be created.
    /// </summary>
    [DataMember(Order = 1, Name = "interval"), JsonPropertyOrder(1), JsonPropertyName("interval"), YamlMember(Alias = "interval", Order = 1)]
    [JsonConverter(typeof(Iso8601NullableTimeSpanConverter))]
    public virtual TimeSpan? Interval { get; set; }

    /// <summary>
    /// Gets/sets a object that represents the CRON expression that defines when the workflow instance should be created
    /// </summary>
    [DataMember(Order = 2, Name = "cron"), JsonPropertyOrder(2), JsonPropertyName("cron"), YamlMember(Alias = "cron", Order = 2)]
    [JsonConverter(typeof(OneOfConverter<CronDefinition, string>))]
    public virtual OneOf<CronDefinition, string>? CronValue { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the schedule following which workflow instances should be created
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual CronDefinition? Cron
    {
        get
        {
            if (this.CronValue?.T1Value == null && !string.IsNullOrWhiteSpace(this.CronValue?.T2Value)) return new() { Expression = this.CronValue.T2Value };
            else return this.CronValue?.T1Value;
        }
        set
        {
            if (value == null) this.CronValue = null;
            else this.CronValue = value;
        }
    }

    /// <summary>
    /// Gets/sets a CRON expression that defines when the workflow instance should be created
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? CronExpression
    {
        get
        {
            if (this.CronValue?.T1Value == null) return this.CronValue?.T2Value;
            else return this.CronValue?.T1Value?.Expression;
        }
        set
        {
            if (value == null) this.CronValue = null;
            else this.CronValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the timezone name used to evaluate the cron expression. Not used for interval as timezone can be specified there directly. If not specified, should default to local machine timezone.
    /// </summary>
    [DataMember(Order = 3, Name = "timezone"), JsonPropertyOrder(3), JsonPropertyName("timezone"), YamlMember(Alias = "timezone", Order = 3)]
    public virtual string? Timezone { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 4, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

}
