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
    /// Represents the <see cref="JsonConverter"/> used to convert from and to <see cref="Any"/> instances
    /// </summary>
    public class AnyConverter
        : JsonConverter<Any>
    {

        /// <inheritdoc/>
        public override Any? ReadJson(JsonReader reader, Type objectType, Any? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JObject.ReadFrom(reader);
            if(token == null
                || token.Type == JTokenType.Null)
                return null;
            var any = new Any();
            if (token is not JObject jobject)
                throw new Exception($"Any expects a value of a non-primitive, complex object type");
            foreach (var property in jobject.Properties())
            {
                var value = JsonConvert.DeserializeObject(property.Value.ToString(Formatting.None));
                any.Set(property.Name, value);
            }
            return any;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, Any? value, JsonSerializer serializer)
        {
            if(value != null)
                serializer.Serialize(writer, value.ToObject());
        }

    }

}
