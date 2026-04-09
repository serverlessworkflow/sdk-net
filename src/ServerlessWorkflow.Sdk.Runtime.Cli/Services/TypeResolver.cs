namespace ServerlessWorkflow.Sdk.Runtime.Cli.Services;

internal sealed class TypeResolver(IServiceProvider serviceProvider) 
    : ITypeResolver
{

    public object? Resolve(Type? type) => type is null ? null : serviceProvider.GetService(type);

}