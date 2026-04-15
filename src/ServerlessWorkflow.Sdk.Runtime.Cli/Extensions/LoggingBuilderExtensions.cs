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

namespace ServerlessWorkflow.Sdk.Runtime.Cli.Extensions;

/// <summary>
/// Defines extensions for configuring the CLI's logging pipeline
/// </summary>
internal static class LoggingBuilderExtensions
{

    const string FileLogOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// Configures Serilog to write to the specified file, if <paramref name="logFile"/> is not null or whitespace
    /// </summary>
    /// <param name="logging">The <see cref="ILoggingBuilder"/> to configure</param>
    /// <param name="logFile">The file to write logs to, if any</param>
    /// <returns>The configured <see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddFileLogging(this ILoggingBuilder logging, string? logFile)
    {
        if (string.IsNullOrWhiteSpace(logFile)) return logging;
        var serilog = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File(
                path: logFile,
                outputTemplate: FileLogOutputTemplate,
                shared: false,
                rollOnFileSizeLimit: false)
            .CreateLogger();
        logging.AddSerilog(serilog, dispose: true);
        return logging;
    }

    /// <summary>
    /// Extracts the value of the <c>-l</c>/<c>--log</c> option from the specified command-line arguments, if any
    /// </summary>
    /// <param name="args">The command-line arguments to inspect</param>
    /// <returns>The log file path, or <see langword="null"/> if not specified</returns>
    public static string? GetLogFileOption(this string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a is "-l" or "--log") return i + 1 < args.Length ? args[i + 1] : null;
            if (a.StartsWith("--log=", StringComparison.OrdinalIgnoreCase)) return a[6..];
            if (a.StartsWith("-l=", StringComparison.OrdinalIgnoreCase)) return a[3..];
        }
        return null;
    }

}
