namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Subflow"/>
/// </summary>
public interface ISubflowActionBuilder
    : IActionBuilder
{

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the latest version of the specified workflow definition
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder LatestVersion();

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the workflow definition with the specified version
    /// </summary>
    /// <param name="version">The version of the workflow definition to run</param>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Version(string version);

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the referenced workflow definition synchronously, which is the default.
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Synchronously();

    /// <summary>
    /// Configures the <see cref="SubflowReference"/> to run the referenced workflow definition asynchronously
    /// </summary>
    /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
    ISubflowActionBuilder Asynchronously();

}
