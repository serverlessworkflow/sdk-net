using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an inject state
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Inject)]
public class InjectStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="InjectStateDefinition"/>
    /// </summary>
    public InjectStateDefinition() : base(StateType.Inject) { }

    /// <summary>
    /// Gets/sets the object to inject within the state's data input and can be manipulated via filter
    /// </summary>
    [DataMember(Order = 5, Name = "data"), JsonPropertyOrder(5), JsonPropertyName("data"), YamlMember(Alias = "data", Order = 5)]
    public virtual object Data { get; set; } = null!;

}
