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
/// Represents a file-based implementation of the <see cref="ISecretsManager"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The service used to access the current <see cref="SecretManagerOptions"/></param>
public sealed class SecretsManager(ILogger<SecretsManager> logger, IOptions<SecretManagerOptions> options)
    : BackgroundService, ISecretsManager
{

    readonly Dictionary<string, JsonNode> secrets = [];

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var path = string.IsNullOrWhiteSpace(options.Value.Directory) ? SecretManagerOptions.DefaultDirectory : options.Value.Directory;
            var directory = new DirectoryInfo(path);
            if (!directory.Exists) directory.Create();
            foreach (var file in directory.GetFiles())
            {
                using var stream = file.OpenRead();
                try
                {
                    var secret = (await JsonSerializer.DeserializeAsync(stream, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonObject, stoppingToken))!;
                    secrets.Add(file.Name, secret);
                }
                catch (Exception ex)
                {
                    logger.LogWarning("Skipped loading secret '{secretFile}': an exception occurred while deserializing the secret object: {ex}", file.Name, ex.Message);
                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning("Failed to load secrets because there are none or because they are improperly configured. Error: {ex}", ex.Message);
        }
    }

    /// <inheritdoc/>
    public Task<IDictionary<string, JsonNode>> GetAsync(CancellationToken cancellationToken = default) => Task.FromResult((IDictionary<string, JsonNode>)secrets);

}