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
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using System;

namespace Newtonsoft.Json.Converters
{

    /// <summary>
    /// Represents the service used to convert <see cref="OneOf{T1, T2}"/>
    /// </summary>
    /// <typeparam name="T1">The first type alternative</typeparam>
    /// <typeparam name="T2">The second type alternative</typeparam>
    public class OneOfConverter<T1, T2>
        : JsonConverter<OneOf<T1, T2>>
    {

        /// <inheritdoc/>
        public override OneOf<T1, T2> ReadJson(JsonReader reader, Type objectType, OneOf<T1, T2> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            try
            {
                return new(token.ToObject<T1>());
            }
            catch
            {
                return new(token.ToObject<T2>());
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, OneOf<T1, T2> value, JsonSerializer serializer)
        {
            if (value.T1Value != null)
                serializer.Serialize(writer, value.T1Value);
            else
                serializer.Serialize(writer, value.T2Value);
        }

    }

}
