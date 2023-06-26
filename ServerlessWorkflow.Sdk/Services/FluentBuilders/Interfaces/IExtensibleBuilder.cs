namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="IExtensible"/>s
/// </summary>
/// <typeparam name="TExtensible">The type of the <see cref="IExtensibleBuilder{TExtensible}"/></typeparam>
public interface IExtensibleBuilder<TExtensible>
     where TExtensible : class, IExtensibleBuilder<TExtensible>
{

    /// <summary>
    /// Adds the specified extension property
    /// </summary>
    /// <param name="name">The extension property name</param>
    /// <param name="value">The extension property value</param>
    /// <returns>The configured container</returns>
    TExtensible WithExtensionProperty(string name, object value);

    /// <summary>
    /// Adds the specified extension property
    /// </summary>
    /// <param name="properties">A name/value mapping of the extension properties to add</param>
    /// <returns>The configured container</returns>
    TExtensible WithExtensionProperties(IDictionary<string, object> properties);

}