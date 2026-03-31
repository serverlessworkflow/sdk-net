namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the JSON serialization context for the Serverless Workflow SDK, providing configuration and metadata for serializing and deserializing all relevant types within the SDK to and from JSON format.
/// </summary>
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(AuthenticationPolicyDefinition))]
[JsonSerializable(typeof(AuthenticationSchemeDefinition))]
[JsonSerializable(typeof(BasicAuthenticationSchemeDefinition))]
[JsonSerializable(typeof(BearerAuthenticationSchemeDefinition))]
[JsonSerializable(typeof(CertificateAuthenticationSchemeDefinition))]
[JsonSerializable(typeof(ComponentDefinition))]
[JsonSerializable(typeof(DigestAuthenticationSchemeDefinition))]
[JsonSerializable(typeof(Duration))]
[JsonSerializable(typeof(EndpointDefinition))]
[JsonSerializable(typeof(ExternalResourceDefinition))]
[JsonSerializable(typeof(InputDataModelDefinition))]
[JsonSerializable(typeof(JsonArray))]
[JsonSerializable(typeof(JsonNode))]
[JsonSerializable(typeof(JsonObject))]
[JsonSerializable(typeof(JsonValue))]
[JsonSerializable(typeof(OAuth2AuthenticationClientDefinition))]
[JsonSerializable(typeof(OAuth2AuthenticationEndpointsDefinition))]
[JsonSerializable(typeof(OAuth2AuthenticationRequestDefinition))]
[JsonSerializable(typeof(OAuth2AuthenticationSchemeDefinition))]
[JsonSerializable(typeof(OAuth2AuthenticationSchemeDefinitionBase))]
[JsonSerializable(typeof(OAuth2TokenDefinition))]
[JsonSerializable(typeof(OpenIDConnectSchemeDefinition))]
[JsonSerializable(typeof(OutputDataModelDefinition))]
[JsonSerializable(typeof(ReferenceableComponentDefinition))]
[JsonSerializable(typeof(SchemaDefinition))]
[JsonSerializable(typeof(SetTaskDefinition))]
[JsonSerializable(typeof(TaskDefinition))]
[JsonSerializable(typeof(TimeoutDefinition))]
[JsonSerializable(typeof(WorkflowDefinition))]
public partial class JsonSerializationContext
    : JsonSerializerContext
{



}
