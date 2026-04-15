// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
