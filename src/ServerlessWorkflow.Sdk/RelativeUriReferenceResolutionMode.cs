namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of reference resolution modes for relative <see cref="Uri"/>s
/// </summary>
public static class RelativeUriReferenceResolutionMode
{

    /// <summary>
    /// Indicates that relative uris should be converted to an absolute one by combining them to a specified base uri
    /// </summary>
    public const string ConvertToAbsolute = "convertToAbsolute";

    /// <summary>
    /// Indicates that relative uris should be converted to a file path relative to a specified base directory
    /// </summary>
    public const string ConvertToRelativeFilePath = "convertToRelativeFilePath";

    /// <summary>
    /// Indicates that relative uris should not be resolved
    /// </summary>
    public const string None = "none";

}
