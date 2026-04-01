namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ContainerProcessDefinitionFactory
{
    internal static ContainerProcessDefinition Create() => new()
    {
        Image = "my-app:latest",
        Name = "my-container",
        Command = "dotnet run",
        Environment = new EquatableDictionary<string, string>(
            new Dictionary<string, string>
            {
                ["ASPNETCORE_ENVIRONMENT"] = "Production"
            }),
        Lifetime = ContainerLifetimeDefinitionFactory.Create()
    };
}
