using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowWriter"/> interface
/// </summary>
public class WorkflowWriter
    : IWorkflowWriter
{

    /// <inheritdoc/>
    public virtual void Write(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        if (stream == null)throw new ArgumentNullException(nameof(stream));
        var input = format switch
        {
            WorkflowDefinitionFormat.Json => Serialization.Serializer.Json.Serialize(workflow),
            WorkflowDefinitionFormat.Yaml => Serialization.Serializer.Yaml.Serialize(workflow),
            _ => throw new NotSupportedException($"The specified workflow definition format '{format}' is not supported"),
        };
        using var streamWriter = new StreamWriter(stream, leaveOpen: true);
        streamWriter.Write(input);
        streamWriter.Flush();
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="IWorkflowWriter"/> interface
    /// </summary>
    /// <returns>A new <see cref="IWorkflowWriter"/></returns>
    public static IWorkflowWriter Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflow();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowWriter>();
    }

}
