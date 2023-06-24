namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IInjectStateBuilder"/> interface
/// </summary>
public class InjectStateBuilder
    : StateBuilder<InjectStateDefinition>, IInjectStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="InjectStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public InjectStateBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <inheritdoc/>
    public virtual IInjectStateBuilder Data(object data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        this.State.Data = data;
        return this;
    }

}
