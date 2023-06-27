namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the base class for all <see cref="SwitchStateDefinition"/> case implementations
/// </summary>
[DataContract, KnownType(nameof(GetKnownTypes))]
public abstract class SwitchCaseDefinition
{

    /// <summary>
    /// Gets the <see cref="SwitchCaseDefinition"/>'s type
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public string OutcomeType => this.Transition == null ? SwitchCaseOutcomeType.End : SwitchCaseOutcomeType.Transition;

    /// <summary>
    /// Gets/sets the <see cref="SwitchCaseDefinition"/>'s name
    /// </summary>
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name"), YamlMember(Alias = "name", Order = 1)]
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets/sets the object that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="TransitionDefinition"/>
    /// </summary>
    [DataMember(Order = 2, Name = "transition"), JsonPropertyOrder(2), JsonPropertyName("transition"), YamlMember(Alias = "transition", Order = 2)]
    [JsonConverter(typeof(OneOfConverter<TransitionDefinition, string>))]
    protected virtual OneOf<TransitionDefinition, string>? TransitionValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual TransitionDefinition? Transition
    {
        get
        {
            if (this.TransitionValue?.T1Value == null && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value)) return new() { NextState = this.TransitionValue.T2Value };
            else return this.TransitionValue?.T1Value;
        }
        set
        {
            if (value == null) this.TransitionValue = null;
            else this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the name of the state definition to transition to upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? TransitionToStateName
    {
        get
        {
            return this.TransitionValue?.T2Value;
        }
        set
        {
            if (value == null) this.TransitionValue = null;
            else this.TransitionValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the object that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="EndDefinition"/>
    /// </summary>
    [DataMember(Order = 3, Name = "end"), JsonPropertyOrder(3), JsonPropertyName("end"), YamlMember(Alias = "end", Order = 3)]
    [JsonConverter(typeof(OneOfConverter<EndDefinition, bool>))]
    protected virtual OneOf<EndDefinition, bool>? EndValue { get; set; }

    /// <summary>
    /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual EndDefinition? End
    {
        get
        {
            if (this.EndValue?.T1Value == null && (this.EndValue != null && this.EndValue.T2Value)) return new() { };
            else return this.EndValue?.T1Value;
        }
        set
        {
            if (value == null) this.EndValue = null;
            else this.EndValue = value;
        }
    }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the state definition is the end of a logicial workflow path
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual bool IsEnd
    {
        get
        {
            if (this.EndValue == null) return false;
            else  return this.EndValue.T2Value;
        }
        set
        {
            this.EndValue = value;
        }
    }

    /// <inheritdoc/>
    public override string? ToString() => string.IsNullOrWhiteSpace(this.Name) ? base.ToString() : this.Name;

    static IEnumerable<Type> GetKnownTypes()
    {
        yield return typeof(DataCaseDefinition);
        yield return typeof(EventCaseDefinition);
        yield return typeof(DefaultCaseDefinition);
    }

}
