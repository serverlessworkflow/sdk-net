#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.UnitTests;

internal static class ObjectAssertionsExtensions
{

    public static void BeJsonEquivalentTo<T>(this ObjectAssertions should, T expected)
    {
        should.BeEquivalentTo(expected, opts => opts
            .Using<JsonElement>(ctx => 
                ctx.Subject.ToJsonString().Should().Be(ctx.Expectation.ToJsonString()))
                .WhenTypeIs<JsonElement>()
            .Using<JsonNode>(ctx =>
                ctx.Subject?.ToJsonString().Should().Be(ctx.Expectation?.ToJsonString()))
                .WhenTypeIs<JsonNode>()
            .Using<JsonObject>(ctx =>
                ctx.Subject?.ToJsonString().Should().Be(ctx.Expectation?.ToJsonString()))
                .WhenTypeIs<JsonObject>()
            .Using<JsonPatch>(ctx =>
                ctx.Subject?.ToJsonDocument().RootElement.ToJsonString().Should().Be(ctx.Expectation?.ToJsonDocument().RootElement.ToJsonString()))
                .WhenTypeIs<JsonPatch>());
    }

}
