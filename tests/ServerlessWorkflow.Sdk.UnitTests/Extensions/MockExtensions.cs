#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.UnitTests;

internal static class MockExtensions
{

    internal static Mock<T> AsIMock<T>(this T obj) where T : class => Mock.Get(obj);

}