using System;
using System.Reflection;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents an <see cref="IObjectFactory"/> implementation that can create instance of objects with non-public parameterless constructors
    /// </summary>
    public class NonPublicConstructorObjectFactory
        : IObjectFactory
    {

        /// <inheritdoc/>
        public virtual object Create(Type type)
        {
            try
            {
                if (type.IsValueType)
                    return Activator.CreateInstance(type);
                ConstructorInfo constructor = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    Type.DefaultBinder,
                    Type.EmptyTypes,
                    null);
                if (constructor.IsPublic)
                    return Activator.CreateInstance(type);
                return constructor.Invoke(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }

}
