/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Defines helper methods to handle <see cref="Enum"/>s
    /// </summary>
    public static class EnumHelper
    {

        /// <summary>
        /// Parses the specified input into the desired <see cref="Enum"/>
        /// </summary>
        /// <param name="value">The value to parse</param>
        /// <param name="enumType">The type of the enum to parse</param>
        /// <returns>The parsed value</returns>
        public static object Parse(string value, Type enumType)
        {
            if (int.TryParse(value, out int intValue))
                return intValue;
            foreach (string name in Enum.GetNames(enumType))
            {
                if (value == name)
                    return Enum.Parse(enumType, value);
                EnumMemberAttribute enumMemberAttribute = enumType.GetField(name)!.GetCustomAttribute<EnumMemberAttribute>()!;
                if (enumMemberAttribute != null)
                    if (value.ToLower() == enumMemberAttribute.Value!.ToLower())
                        return Enum.Parse(enumType, name);
            }
            return enumType.GetDefaultValue()!;
        }

        /// <summary>
        /// Parses the specified input into the desired enum
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum to parse</typeparam>
        /// <param name="value">The value to parse</param>
        /// <returns>The parsed value</returns>
        public static TEnum Parse<TEnum>(string value)
        {
            return (TEnum)Parse(value, typeof(TEnum));
        }

        /// <summary>
        /// Gets the string representation for the specified enum value
        /// </summary>
        /// <param name="value">The value to stringify</param>
        /// <param name="enumType">The type of the enum to stringify</param>
        /// <returns>The string representation for the specified enum value</returns>
        public static string Stringify(object value, Type enumType)
        {
            var name = Enum.GetName(enumType, value)!;
            var enumMemberAttribute = enumType.GetField(name)!.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMemberAttribute != null)
                name = enumMemberAttribute.Value;
            return name!;
        }

        /// <summary>
        /// Gets the string representation for the specified enum value
        /// </summary>
        /// <param name="value">The value to stringify</param>
        /// <typeparam name="TEnum">The type of the enum to stringify</typeparam>
        /// <returns>The string representation for the specified enum value</returns>
        public static string Stringify<TEnum>(TEnum value)
        {
            return Stringify(value, typeof(TEnum));
        }

        /// <summary>
        /// Gets all the enum values contained by the specified flags value
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration to get the flags of</typeparam>
        /// <param name="flags">The flags to get the values of</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all the enum values contained by the specifed flags value</returns>
        public static IEnumerable<TEnum> GetFlags<TEnum>(TEnum flags)
            where TEnum : Enum
        {
            foreach (Enum value in Enum.GetValues(typeof(TEnum)))
            {
                if (flags.HasFlag(value))
                    yield return flags;
            }
        }

    }

}
