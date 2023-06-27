namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a <see cref="List{T}"/> that can be loaded from an external definition file
/// </summary>
/// <typeparam name="T">The type of elements contained by the <see cref="ExternalDefinitionCollection{T}"/></typeparam>
public class ExternalDefinitionCollection<T>
    : List<T>
{

    /// <summary>
    /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
    /// </summary>
    public ExternalDefinitionCollection()
        : base()
    {
        this.DefinitionUri = null!;
        this.Loaded = true;
    }

    /// <summary>
    /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
    /// </summary>
    /// <param name="collection">The collection whose elements are copied into the <see cref="ExternalDefinitionCollection{T}"/></param>
    public ExternalDefinitionCollection(IEnumerable<T> collection)
        : base(collection)
    {
        this.DefinitionUri = null!;
        this.Loaded = true;
    }

    /// <summary>
    /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
    /// </summary>
    /// <param name="definitionUri">The <see cref="Uri"/> used to reference the file that defines the elements contained by the <see cref="ExternalDefinitionCollection{T}"/></param>
    public ExternalDefinitionCollection(Uri definitionUri)
        : base()
    {
        this.DefinitionUri = definitionUri;
        this.Loaded = false;
    }

    /// <summary>
    /// Gets the <see cref="Uri"/> used to reference the file that defines the elements contained by the <see cref="ExternalDefinitionCollection{T}"/>
    /// </summary>
    public virtual Uri DefinitionUri { get; private set; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="ExternalDefinitionCollection{T}"/> has been loaded
    /// </summary>
    public virtual bool Loaded { get; }

}
