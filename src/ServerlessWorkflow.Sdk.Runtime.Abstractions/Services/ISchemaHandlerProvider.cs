namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to provide <see cref="ISchemaHandler"/>s
/// </summary>
public interface ISchemaHandlerProvider
{

    /// <summary>
    /// Gets the first registered <see cref="ISchemaHandler"/> that supports the specified schema format
    /// </summary>
    /// <param name="format">The schema format to get an <see cref="ISchemaHandler"/> for</param>
    /// <returns>The first registered <see cref="ISchemaHandler"/>, if any, that supports the specified schema format</returns>
    ISchemaHandler? GetHandler(string format);

}
