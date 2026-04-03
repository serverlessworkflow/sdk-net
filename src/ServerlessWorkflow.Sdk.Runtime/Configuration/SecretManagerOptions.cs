namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure secret management
/// </summary>
[DataContract]
public sealed class SecretManagerOptions
{

    /// <summary>
    /// Gets the default directory where to locate secrets
    /// </summary>
    public static readonly string DefaultDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "secrets");

    /// <summary>
    /// Gets/sets the directory where secrets are located
    /// </summary>
    [Description("The directory where the runner's secrets are located")]
    [DataMember(Order = 1, Name = "directory"), JsonPropertyOrder(1), JsonPropertyName("directory")]
    public string? Directory { get; set; }

}
