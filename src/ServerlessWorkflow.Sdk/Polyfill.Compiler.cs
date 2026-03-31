#pragma warning disable IDE0130 // Namespace does not match folder structure
#pragma warning disable CS9113 // Parameter is unread.
namespace System.Runtime.CompilerServices;

internal static class IsExternalInit { }

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
internal sealed class RequiredMemberAttribute : Attribute { }

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
internal sealed class CompilerFeatureRequiredAttribute(string featureName) : Attribute { }