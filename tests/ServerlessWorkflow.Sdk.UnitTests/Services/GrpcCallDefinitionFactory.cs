using ServerlessWorkflow.Sdk.Models.Calls;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class GrpcCallDefinitionFactory
{
    internal static GrpcCallDefinition Create() => new()
    {
        Proto = ExternalResourceDefinitionFactory.Create(),
        Service = GrpcServiceDefinitionFactory.Create(),
        Method = "SayHello",
        Arguments = new JsonObject { ["name"] = "world" }
    };
}
