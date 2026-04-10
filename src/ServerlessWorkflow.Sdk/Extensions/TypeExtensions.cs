#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="Type"/>s
/// </summary>
public static class TypeExtensions
{

    /// <summary>
    /// Determines whether the extended <see cref="Type"/> is an <see cref="IEnumerable"/>
    /// </summary>
    /// <param name="extended">The type to check</param>
    /// <returns>A boolean indicating whether the extended <see cref="Type"/> is an <see cref="IEnumerable"/></returns>
    public static bool IsEnumerable(this Type extended) => typeof(IEnumerable).IsAssignableFrom(extended);

    /// <summary>
    /// Gets the type's generic type of the specified generic type definition
    /// </summary>
    /// <param name="extended">The extended type</param>
    /// <param name="genericTypeDefinition">The generic type definition to get the generic type of</param>
    /// <returns>The type's generic type of the specified generic type definition</returns>
    public static Type? GetGenericType(this Type extended, Type genericTypeDefinition)
    {
        if (genericTypeDefinition is null) throw new ArgumentNullException(nameof(genericTypeDefinition));
        Type? baseType, result;
        if (!genericTypeDefinition.IsGenericTypeDefinition) throw new ArgumentException("The specified type is not a generic type definition", nameof(genericTypeDefinition));
        baseType = extended;
        while (baseType != null)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericTypeDefinition) return baseType;
            result = baseType.GetInterfaces().Select(i => i.GetGenericType(genericTypeDefinition)).Where(t => t != null).FirstOrDefault();
            if (result != null) return result;
            baseType = baseType.BaseType;
        }
        return null;
    }

}