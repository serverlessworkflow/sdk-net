namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class DateTimeDescriptorFactory
{
    internal static DateTimeDescriptor Create() => new()
    { 
        Iso8601 = "2024-06-01T12:00:00Z",
        Epoch = EpochFactory.Create()
    };
}
