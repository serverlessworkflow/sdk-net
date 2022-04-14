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

namespace System.Text.Json.Serialization.Converters
{

    /// <summary>
    /// Represents the <see cref="JsonConverter{T}"/> used to convert <see cref="TimeSpan"/>s from and to ISO 8601 durations
    /// </summary>
    public class Iso8601TimeSpanConverter
        : JsonConverter<TimeSpan>
    {

        /// <inheritdoc/>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var iso8601Input = reader.GetString();
            if (string.IsNullOrWhiteSpace(iso8601Input))
                return TimeSpan.Zero;
            return Iso8601TimeSpan.Parse(iso8601Input);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

    }

    /// <summary>
    /// Represents the <see cref="JsonConverter{T}"/> used to convert <see cref="TimeSpan"/>s from and to ISO 8601 durations
    /// </summary>
    public class Iso8601NullableTimeSpanConverter
        : JsonConverter<TimeSpan?>
    {

        /// <inheritdoc/>
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var iso8601Input = reader.GetString();
            if (string.IsNullOrWhiteSpace(iso8601Input))
                return null;
            return Iso8601TimeSpan.Parse(iso8601Input);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if(value.HasValue)
                writer.WriteStringValue(value.ToString());
        }

    }

}
