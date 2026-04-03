using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CloudEventFactory
{
    internal static CloudEvent Create() => new()
    {
        Id = "ce-123",
        SpecVersion = CloudEvent.DefaultVersion,
        Time = new DateTimeOffset(2026, 4, 3, 12, 0, 0, TimeSpan.Zero),
        Source = new Uri("https://example.com/source"),
        Type = "com.example.test",
        Subject = "test-subject",
        DataContentType = "application/json",
        DataSchema = new Uri("https://example.com/schema"),
        Data = new JsonObject { ["key"] = "value" }
    };
}
