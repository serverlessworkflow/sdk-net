using System.Reflection;
using System.Text.Json.Serialization.Metadata;

namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Defines extension modifiers for <see cref="IJsonTypeInfoResolver"/>s
/// </summary>
/// <remarks>Code taken from https://stackoverflow.com/a/76296944/3637555</remarks>
public static partial class JsonTypeInfoModifiers
{

    /// <summary>
    /// Includes non public properties in the specified <see cref="JsonTypeInfo"/>
    /// </summary>
    /// <returns>A new <see cref="Action{T}"/> used to modify a <see cref="JsonTypeInfo"/> by including non-public properties</returns>
    public static Action<JsonTypeInfo> IncludeNonPublicProperties() => typeInfo =>
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object) return;
        foreach (var type in typeInfo.Type.BaseTypesAndSelf().TakeWhile(b => b != typeof(object)))
            IncludeNonPublicProperties(typeInfo, type, p => Attribute.IsDefined(p, typeof(JsonPropertyNameAttribute)));
    };

    /// <summary>
    /// Includes non public properties in the specified <see cref="JsonTypeInfo"/>
    /// </summary>
    /// <param name="declaredType">The type to include the non-public properties of</param>
    /// <returns>A new <see cref="Action{T}"/> used to modify a <see cref="JsonTypeInfo"/> by including non-public properties</returns>
    public static Action<JsonTypeInfo> IncludeNonPublicProperties(Type declaredType) => typeInfo =>
        IncludeNonPublicProperties(typeInfo, declaredType, p => true);

    /// <summary>
    /// Includes the specified non-public property
    /// </summary>
    /// <param name="declaredType">The type that defines the property to include</param>
    /// <param name="propertyName">The name of the property to include</param>
    /// <returns>A new <see cref="Action{T}"/> used to modify a <see cref="JsonTypeInfo"/> by including non-public properties</returns>
    public static Action<JsonTypeInfo> IncludeNonPublicProperty(Type declaredType, string propertyName) => typeInfo =>
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object || !declaredType.IsAssignableFrom(typeInfo.Type)) return;
        var propertyInfo = declaredType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new ArgumentException(string.Format("Private roperty {0} not found in type {1}", propertyName, declaredType));
        if (typeInfo.Properties.Any(p => p.GetMemberInfo() == propertyInfo)) return;
        IncludeProperty(typeInfo, propertyInfo);
    };

    static void IncludeNonPublicProperties(JsonTypeInfo typeInfo, Type declaredType, Func<PropertyInfo, bool> filter)
    {
        if (typeInfo.Kind != JsonTypeInfoKind.Object || !declaredType.IsAssignableFrom(typeInfo.Type)) return;
        var propertyInfos = declaredType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var propertyInfo in propertyInfos.Where(p => p.GetIndexParameters().Length == 0 && filter(p)))
            IncludeProperty(typeInfo, propertyInfo);
    }

    static void IncludeProperty(JsonTypeInfo typeInfo, PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetIndexParameters().Length > 0) throw new ArgumentException("Indexed properties are not supported.");
        var ignore = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
        if (ignore?.Condition == JsonIgnoreCondition.Always) return;
        var name = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
            ?? typeInfo.Options?.PropertyNamingPolicy?.ConvertName(propertyInfo.Name)
            ?? propertyInfo.Name;
        var property = typeInfo.Properties.FirstOrDefault(p => p.Name == name);
        if (property != null) typeInfo.Properties.Remove(property);
        property = typeInfo.CreateJsonPropertyInfo(propertyInfo.PropertyType, name);
        property.Get = CreateGetter(typeInfo.Type, propertyInfo.GetGetMethod(true));
        property.Set = CreateSetter(typeInfo.Type, propertyInfo.GetSetMethod(true));
        property.AttributeProvider = propertyInfo;
        property.CustomConverter = propertyInfo.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType is { } converterType
            ? (JsonConverter?)Activator.CreateInstance(converterType)
            : null;
        property.Order = propertyInfo.GetCustomAttribute<JsonPropertyOrderAttribute>()?.Order ?? 0;
        property.IsExtensionData = propertyInfo.GetCustomAttribute<JsonExtensionDataAttribute>() != null;
        typeInfo.Properties.Add(property);
    }

    delegate TValue RefFunc<TObject, TValue>(ref TObject arg);

    static Func<object, object?>? CreateGetter(Type type, MethodInfo? method)
    {
        if (method == null) return null;
        var myMethod = typeof(JsonTypeInfoModifiers).GetMethod(nameof(JsonTypeInfoModifiers.CreateGetterGeneric), BindingFlags.NonPublic | BindingFlags.Static)!;
        return (Func<object, object?>)(myMethod.MakeGenericMethod(new[] { type, method.ReturnType }).Invoke(null, new[] { method })!);
    }

    static Func<object, object?> CreateGetterGeneric<TObject, TValue>(MethodInfo method)
    {
        if (method == null) throw new ArgumentNullException(nameof(method));
        if (typeof(TObject).IsValueType)
        {
            var func = (RefFunc<TObject, TValue>)Delegate.CreateDelegate(typeof(RefFunc<TObject, TValue>), null, method);
            return (o) => { var tObj = (TObject)o; return func(ref tObj); };
        }
        else
        {
            var func = (Func<TObject, TValue>)Delegate.CreateDelegate(typeof(Func<TObject, TValue>), method);
            return (o) => func((TObject)o);
        }
    }

    static Action<object, object?>? CreateSetter(Type type, MethodInfo? method)
    {
        if (method == null) return null;
        var myMethod = typeof(JsonTypeInfoModifiers).GetMethod(nameof(JsonTypeInfoModifiers.CreateSetterGeneric), BindingFlags.NonPublic | BindingFlags.Static)!;
        return (Action<object, object?>)(myMethod.MakeGenericMethod(new[] { type, method.GetParameters().Single().ParameterType }).Invoke(null, new[] { method })!);
    }

    static Action<object, object?>? CreateSetterGeneric<TObject, TValue>(MethodInfo method)
    {
        if (method == null) throw new ArgumentNullException(nameof(method));
        if (typeof(TObject).IsValueType)
        {
            return (o, v) => method.Invoke(o, new[] { v });
        }
        else
        {
            var func = (Action<TObject, TValue?>)Delegate.CreateDelegate(typeof(Action<TObject, TValue?>), method);
            return (o, v) => func((TObject)o, (TValue?)v);
        }
    }

    static MemberInfo? GetMemberInfo(this JsonPropertyInfo property) => (property.AttributeProvider as MemberInfo);

    static IEnumerable<Type> BaseTypesAndSelf(this Type? type)
    {
        while (type != null)
        {
            yield return type;
            type = type.BaseType;
        }
    }

}