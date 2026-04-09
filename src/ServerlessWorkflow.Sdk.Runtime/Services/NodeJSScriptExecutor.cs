namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the NodeJS implementation of the <see cref="IScriptExecutor"/> interface
/// </summary>
public sealed class NodeJSScriptExecutor
    : IScriptExecutor
{

    /// <inheritdoc/>
    public bool Supports(string language) => language.Equals("js", StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public async Task<Process> ExecuteAsync(string script, IEnumerable<string>? arguments = null, IDictionary<string, string>? environment = null, CancellationToken cancellationToken = default)
    {
        var guid = Guid.NewGuid().ToString("N")[15..];
        var directory = Path.Combine(Path.GetTempPath(), guid);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        var fileName = "script.js";
        await File.WriteAllTextAsync(Path.Combine(directory, fileName), script, cancellationToken).ConfigureAwait(false);
        var processStart = new ProcessStartInfo()
        {
            FileName = "node",
            WorkingDirectory = directory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        processStart.ArgumentList.Add(fileName);
        arguments?.ToList().ForEach(processStart.ArgumentList.Add);
        environment?.ToList().ForEach(e => processStart.Environment[e.Key] = e.Value);
        return Process.Start(processStart) ?? throw new NullReferenceException("Failed to create a new process to evaluate the script");
    }

}
