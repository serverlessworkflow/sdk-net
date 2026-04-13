namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an <see cref="IRuntimeExpressionEvaluator"/> that uses the JQ language to evaluate expressions
/// </summary>
public sealed class JQRuntimeExpressionEvaluator
    : IRuntimeExpressionEvaluator
{

    /// <inheritdoc/>
    public bool Supports(string language) => language.Trim().Equals(RuntimeExpressions.Languages.JQ, StringComparison.OrdinalIgnoreCase);

    /// <inheritdoc/>
    public async Task<JsonNode?> EvaluateAsync(string expression, JsonNode input, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        ArgumentNullException.ThrowIfNull(input);
        expression = expression.Trim();
        if (expression.StartsWith("${")) expression = expression[2..^1].Trim();
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        var startInfo = new ProcessStartInfo()
        {
            FileName = "jq",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        startInfo.ArgumentList.Add(expression);
        if (arguments is not null) foreach (var property in arguments)
        {
            startInfo.ArgumentList.Add("--argjson");
            startInfo.ArgumentList.Add(property.Key);
            startInfo.ArgumentList.Add(JsonSerializer.Serialize(property.Value, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonNode));
        }
        var files = new List<string>();
        var maxLength = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 8000 : 32699;
        if (startInfo.ArgumentList.Any(a => a.Length >= maxLength))
        {
            startInfo.ArgumentList.Clear();
            var filterFile = Path.GetTempFileName();
            File.WriteAllText(filterFile, expression);
            files.Add(filterFile);
            startInfo.ArgumentList.Add("-f");
            startInfo.ArgumentList.Add(filterFile);
            if (arguments is not null) foreach (var property in arguments)
            {
                var argFile = Path.GetTempFileName();
                File.WriteAllText(argFile, JsonSerializer.Serialize(property.Value, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonNode));
                files.Add(argFile);
                startInfo.ArgumentList.Add("--argfile");
                startInfo.ArgumentList.Add(property.Key);
                startInfo.ArgumentList.Add(argFile);
            }
        }
        startInfo.ArgumentList.Add("-c");
        using var process = new Process()
        {
            StartInfo = startInfo
        };
        var cancellationRegistration = cancellationToken.Register(() =>
        {
            try
            {
                process.Kill();
            }
            catch { }
        });
        process.Start();
        process.StandardInput.Write(JsonSerializer.Serialize(input, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonNode));
        process.StandardInput.Close();
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        cancellationRegistration.Unregister();
        cancellationRegistration.Dispose();
        foreach (var file in files) try { File.Delete(file); } catch { }
        if (process.ExitCode != 0) throw new Exception($"An error occurred while evaluating the specified expression: {error}");
        if (string.IsNullOrWhiteSpace(output)) return null;
        try { return JsonSerializer.Deserialize(output, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonNode); }
        catch (JsonException ex) { throw new Exception($"An error occurred while deserializing the output of the expression evaluation: {ex.Message}"); }
    }

}
