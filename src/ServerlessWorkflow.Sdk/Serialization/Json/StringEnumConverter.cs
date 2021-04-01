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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Text.Json.Serialization.Converters
{

    /// <summary>
    /// Represents the <see cref="JsonConverter{T}"/> used to convert from and to <see cref="Enum"/>s
    /// </summary>
    /// <typeparam name="T">The type to convert</typeparam>
    public class StringEnumConverter<T>
        : JsonConverter<T>
    {

        /// <summary>
        /// Initializes a new <see cref="StringEnumConverter{TEnum}"/>
        /// </summary>
        /// <param name="underlyingType">The underlying <see cref="Enum"/>'s type</param>
        /// <param name="jsonSerializerOptions">The current <see cref="Json.JsonSerializerOptions"/></param>
        public StringEnumConverter(Type underlyingType, JsonSerializerOptions jsonSerializerOptions)
        {
            this.UnderlyingType = underlyingType;
            this.JsonSerializerOptions = jsonSerializerOptions;
            Array values = this.UnderlyingType.GetEnumValues();
            string[] names = this.UnderlyingType.GetEnumNames();
            this.TypeCode = Type.GetTypeCode(this.UnderlyingType);
            this.IsFlags = this.UnderlyingType.IsDefined(typeof(FlagsAttribute), true);
            this.ValueMappings = new Dictionary<ulong, EnumFieldMetadata>();
            this.NameMappings = new Dictionary<string, EnumFieldMetadata>();
            for (int index = 0; index < values.Length; index++)
            {
                Enum value = (Enum)values.GetValue(index);
                ulong rawValue = this.GetRawValue(value);
                string name = names[index];
                FieldInfo field = this.UnderlyingType.GetField(name, BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                EnumMemberAttribute enumMemberAttribute = field.GetCustomAttribute<EnumMemberAttribute>(true);
                string transformedName = enumMemberAttribute?.Value ?? this.JsonSerializerOptions?.PropertyNamingPolicy.ConvertName(name) ?? name;
                EnumFieldMetadata fieldMetadata = new EnumFieldMetadata(name, transformedName, value, rawValue);
                if (!this.ValueMappings.ContainsKey(rawValue))
                    this.ValueMappings.Add(rawValue, fieldMetadata);
                this.NameMappings.Add(transformedName, fieldMetadata);
            }
        }

        /// <summary>
        /// Gets the underlying <see cref="Enum"/>'s type
        /// </summary>
        protected Type UnderlyingType { get; }

        /// <summary>
        /// Gets the current <see cref="JsonSerializerOptions"/>
        /// </summary>
        protected JsonSerializerOptions JsonSerializerOptions { get; }

        /// <summary>
        /// Gets the <see cref="Enum"/>'s <see cref="System.TypeCode"/>
        /// </summary>
        protected TypeCode TypeCode { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not the specified <see cref="Enum"/> is flags
        /// </summary>
        protected bool IsFlags { get; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing mappings of raw values to field metadata
        /// </summary>
        protected Dictionary<ulong, EnumFieldMetadata> ValueMappings { get; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing mappings of names to field metadata
        /// </summary>
        protected Dictionary<string, EnumFieldMetadata> NameMappings { get; }

        /// <inheritdoc/>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    string enumString = reader.GetString();
                    if (this.NameMappings.TryGetValue(enumString, out EnumFieldMetadata fieldMetadata))
                        return (T)Enum.ToObject(this.UnderlyingType, fieldMetadata.RawValue);
                    if (this.IsFlags)
                    {
                        ulong rawValue = 0;
                        string[] flagValues = enumString.Split(", ");
                        foreach (string flagValue in flagValues)
                        {
                            if (this.NameMappings.TryGetValue(flagValue, out fieldMetadata))
                            {
                                rawValue |= fieldMetadata.RawValue;
                            }
                            else
                            {
                                bool matched = false;
                                foreach (KeyValuePair<string, EnumFieldMetadata> kvp in this.NameMappings)
                                {
                                    if (string.Equals(kvp.Key, flagValue, StringComparison.OrdinalIgnoreCase))
                                    {
                                        rawValue |= kvp.Value.RawValue;
                                        matched = true;
                                        break;
                                    }
                                }

                                if (!matched)
                                    throw new JsonException($"Unknown flag value {flagValue}.");
                            }
                        }
                        return (T)Enum.ToObject(this.UnderlyingType, rawValue);
                    }
                    foreach (KeyValuePair<string, EnumFieldMetadata> kvp in this.NameMappings)
                    {
                        if (string.Equals(kvp.Key, enumString, StringComparison.OrdinalIgnoreCase))
                        {
                            return (T)Enum.ToObject(this.UnderlyingType, kvp.Value.RawValue);
                        }
                    }
                    throw new JsonException($"Unknown {this.UnderlyingType} value {enumString}");
                case JsonTokenType.Number:
                    switch (this.TypeCode)
                    {
                        case TypeCode.Int32:
                            if (reader.TryGetInt32(out int int32))
                                return (T)Enum.ToObject(this.UnderlyingType, int32);
                            break;
                        case TypeCode.UInt32:
                            if (reader.TryGetUInt32(out uint uint32))
                                return (T)Enum.ToObject(this.UnderlyingType, uint32);
                            break;
                        case TypeCode.UInt64:
                            if (reader.TryGetUInt64(out ulong uint64))
                                return (T)Enum.ToObject(this.UnderlyingType, uint64);
                            break;
                        case TypeCode.Int64:
                            if (reader.TryGetInt64(out long int64))
                                return (T)Enum.ToObject(this.UnderlyingType, int64);
                            break;
                        case TypeCode.SByte:
                            if (reader.TryGetSByte(out sbyte byte8))
                                return (T)Enum.ToObject(this.UnderlyingType, byte8);
                            break;
                        case TypeCode.Byte:
                            if (reader.TryGetByte(out byte ubyte8))
                                return (T)Enum.ToObject(this.UnderlyingType, ubyte8);
                            break;
                        case TypeCode.Int16:
                            if (reader.TryGetInt16(out short int16))
                            {
                                return (T)Enum.ToObject(this.UnderlyingType, int16);
                            }
                            break;
                        case TypeCode.UInt16:
                            if (reader.TryGetUInt16(out ushort uint16))
                                return (T)Enum.ToObject(this.UnderlyingType, uint16);
                            break;
                    }
                    throw new JsonException($"Unsupported type code '{this.TypeCode}'");
                default:
                    throw new JsonException($"Unsupported token type '{reader.TokenType}'");
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            ulong rawValue = this.GetRawValue(value);
            if (this.ValueMappings.TryGetValue(rawValue, out EnumFieldMetadata fieldMetadata))
            {
                writer.WriteStringValue(fieldMetadata.TransformedName);
                return;
            }
            if (this.IsFlags)
            {
                ulong flagsValue = 0;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (KeyValuePair<ulong, EnumFieldMetadata> kvp in this.ValueMappings)
                {
                    fieldMetadata = kvp.Value;
                    if ((value as Enum)!.HasFlag(fieldMetadata.Value)
                        || fieldMetadata.RawValue == 0)
                        continue;
                    flagsValue |= fieldMetadata.RawValue;
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append(", ");
                    stringBuilder.Append(fieldMetadata.TransformedName);
                }
                if (flagsValue == rawValue)
                {
                    writer.WriteStringValue(stringBuilder.ToString());
                    return;
                }
            }
            switch (this.TypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue((int)rawValue);
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue((uint)rawValue);
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(rawValue);
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue((long)rawValue);
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue((short)rawValue);
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue((ushort)rawValue);
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue((byte)rawValue);
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue((sbyte)rawValue);
                    break;
                default:
                    throw new JsonException();
            }
        }

        private ulong GetRawValue(object value)
        {
            switch (this.TypeCode)
            {
                case TypeCode.Int32:
                    return (ulong)(int)value;
                case TypeCode.UInt32:
                    return (uint)value;
                case TypeCode.UInt64:
                    return (ulong)value;
                case TypeCode.Int64:
                    return (ulong)(long)value;
                case TypeCode.SByte:
                    return (ulong)(sbyte)value;
                case TypeCode.Byte:
                    return (byte)value;
                case TypeCode.Int16:
                    return (ulong)(short)value;
                case TypeCode.UInt16:
                    return (ushort)value;
                default:
                    throw new JsonException($"Unsupported type code '{this.TypeCode}'");
            }
        }

        /// <summary>
        /// Holds information about an <see cref="Enum"/>'s field
        /// </summary>
        protected class EnumFieldMetadata
        {

            /// <summary>
            /// Initializes a new <see cref="EnumFieldMetadata"/>
            /// </summary>
            /// <param name="name">The <see cref="Enum"/> field's name</param>
            /// <param name="transformedName">The <see cref="Enum"/> field's transformed name</param>
            /// <param name="value">The <see cref="Enum"/> field's value</param>
            /// <param name="rawValue">The <see cref="Enum"/> field's raw value</param>
            public EnumFieldMetadata(string name, string transformedName, Enum value, ulong rawValue)
            {
                this.Name = name;
                this.TransformedName = transformedName;
                this.Value = value;
                this.RawValue = rawValue;
            }

            /// <summary>
            /// Gets the <see cref="Enum"/>'s field name
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets the <see cref="Enum"/> field's transformed name
            /// </summary>
            public string TransformedName { get; }

            /// <summary>
            /// Gets the <see cref="Enum"/> field's value
            /// </summary>
            public Enum Value { get; }

            /// <summary>
            /// Gets the <see cref="Enum"/> field's raw value
            /// </summary>
            public ulong RawValue { get; }

        }

    }

}
