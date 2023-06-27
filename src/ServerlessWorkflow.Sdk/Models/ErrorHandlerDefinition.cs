namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an error handler
/// </summary>
[DataContract]
public class ErrorHandlerDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets a domain-specific error name, or '*' to indicate all possible errors. If other handlers are declared, the <see cref="ErrorHandlerDefinition"/> will only be considered on errors that have NOT been handled by any other.
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "error"), JsonPropertyOrder(1), JsonPropertyName("error"), YamlMember(Alias = "error", Order = 1)]
    public virtual string Error { get; set; } = null!;

    /// <summary>
    /// Gets/sets the error code. Can be used in addition to the name to help runtimes resolve to technical errors/exceptions. Should not be defined if error is set to '*'.
    /// </summary>
    [DataMember(Order = 2, Name = "code"), JsonPropertyOrder(2), JsonPropertyName("code"), YamlMember(Alias = "code", Order = 2)]
    public virtual string? Code { get; set; }

    /// <summary>
    /// Gets/sets a reference to the <see cref="RetryDefinition"/> to use 
    /// </summary>
    [DataMember(Order = 3, Name = "retryRef"), JsonPropertyOrder(3), JsonPropertyName("retryRef"), YamlMember(Alias = "retryRef", Order = 3)]
    public virtual string? RetryRef { get; set; } = null!;

    /// <summary>
    /// Gets/sets the object that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="TransitionDefinition"/>
    /// </summary>
    [DataMember(Order = 4, Name = "transition"), JsonPropertyOrder(4), JsonPropertyName("transition"), YamlMember(Alias = "transition", Order = 4)]
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
    /// Gets/sets the object that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="EndDefinition"/>
    /// </summary>
    [DataMember(Order = 5, Name = "end"), JsonPropertyOrder(5), JsonPropertyName("end"), YamlMember(Alias = "end", Order = 5)]
    public virtual object? End { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 6, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, object>? ExtensionData { get; set; }

    /// <inheritdoc/>
    public override string ToString() => $"{this.Error}{(string.IsNullOrWhiteSpace(this.Code) ? string.Empty : $" (code: '{this.Code}')")}";

}
