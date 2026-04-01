#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Represents a <see cref="ValidationAttribute"/> used to validate semantic versions
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class SemanticVersionAttribute()
    : RegularExpressionAttribute(@"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$")
{



}
