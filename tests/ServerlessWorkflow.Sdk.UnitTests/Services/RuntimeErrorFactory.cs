using ServerlessWorkflow.Sdk.Runtime.Models;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RuntimeErrorFactory
{
    internal static Error Create() => new()
    {
        Type = ErrorType.Runtime,
        Title = ErrorTitle.Runtime,
        Status = ErrorStatus.Runtime,
        Detail = "An unexpected error occurred during task execution",
        Instance = new Uri("/tasks/123", UriKind.RelativeOrAbsolute)
    };
}
