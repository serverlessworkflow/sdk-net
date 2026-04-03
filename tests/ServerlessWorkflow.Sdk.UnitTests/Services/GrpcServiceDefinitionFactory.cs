namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class GrpcServiceDefinitionFactory
{
    internal static GrpcServiceDefinition Create() => new()
    {
        Name = "GreeterService",
        Host = "grpc.example.com",
        Port = 443
    };
}
