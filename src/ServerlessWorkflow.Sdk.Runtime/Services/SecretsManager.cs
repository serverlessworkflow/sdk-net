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