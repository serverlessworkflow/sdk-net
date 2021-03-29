/*
 * Copyright 2020-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using System.Reflection;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Exposes extensions for <see cref="Type"/>s
    /// </summary>
    public static class TypeExtensions
    {

        /// <summary>
        /// Gets a boolean indicating whether or not the type is a nullable type
        /// </summary>
        /// <param name="extended">The extended type</param>
        /// <returns>A boolean indicating whether or not the type is a nullable type</returns>
        public static bool IsNullable(this Type extended)
        {
            Type type = extended;
            do
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return true;
                type = type.BaseType;
            }
            while (type != null);
            return false;
        }

        /// <summary>
        /// Attempts to get a custom attribute of the specified type
        /// </summary>
        /// <typeparam name="TAttribute">The type of the custom attribute to get</typeparam>
        /// <param name="extended">The extended type</param>
        /// <param name="attribute">The resulting custom attribute</param>
        /// <returns>A boolean indicating whether or not the custom attribute of the specified type could be found</returns>
        public static bool TryGetCustomAttribute<TAttribute>(this Type extended, out TAttribute attribute)
            where TAttribute : Attribute
        {
            attribute = extended.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }

        /// <summary>
        /// Gets the type's default value
        /// </summary>
        /// <param name="extended">The extended type</param>
        /// <returns>The type's default value</returns>
        public static object GetDefaultValue(this Type extended)
        {
            if (extended.IsValueType)
                return Activator.CreateInstance(extended);
            else
                return null;
        }

    }

}
