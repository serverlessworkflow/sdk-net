namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure an extension authentication mechanism
/// </summary>
[DataContract]
public class ExtensionAuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public ExtensionAuthenticationProperties() { }

    /// <inheritdoc/>
    public ExtensionAuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

}
