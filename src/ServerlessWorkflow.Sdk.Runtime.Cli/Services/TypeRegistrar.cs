namespace ServerlessWorkflow.Sdk.Runtime.Cli.Services;

internal sealed class TypeRegistrar(IServiceProvider serviceProvider)
    : ITypeRegistrar
{

    public void Register(Type service, Type implementation) { }

    public void RegisterInstance(Type service, object implementation) { }

    public void RegisterLazy(Type service, Func<object> factory) { }

    public ITypeResolver Build() => new TypeResolver(serviceProvider);

}
