namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to handle <see cref="SchemaDefinition"/>s
/// </summary>
public interface ISchemaHandler
{

    /// <summary>
    /// Determines whether or not the <see cref="ISchemaHandler"/> supports the specified schema format
    /// </summary>
    /// <param name="format">The format to check</param>
    /// <returns>A boolean indicating whether or not the <see cref="ISchemaHandler"/> supports the specified schema format</returns>
    bool Supports(string format);

    /// <summary>
    /// Validates an object against the specified schema
    /// </summary>
    /// <param name="graph">The object to validate</param>
    /// <param name="schema">The schema to validate the graph against</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An object that describes the validation result</returns>
    Task<ISchemaValidationResult> ValidateAsync(JsonNode graph, SchemaDefinition schema, CancellationToken cancellationToken = default);

}
