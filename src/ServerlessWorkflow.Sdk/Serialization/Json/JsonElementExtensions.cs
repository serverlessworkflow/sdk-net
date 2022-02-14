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

namespace System.Text.Json.Serialization
{
    /// <summary>
    /// Defines extensions for <see cref="JsonElement"/>s
    /// </summary>
    public static class JsonElementExtensions
    {

        /// <summary>
        /// Converts the <see cref="JsonElement"/> into a new object of the specified type
        /// </summary>
        /// <typeparam name="T">The type of object to convert the <see cref="JsonElement"/> into</typeparam>
        /// <param name="element">The <see cref="JsonElement"/> to convert</param>
        /// <returns>A new object of the specified type</returns>
        public static T? ToObject<T>(this JsonElement element)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Converts the <see cref="JsonDocument"/> into a new object of the specified type
        /// </summary>
        /// <typeparam name="T">The type of object to convert the <see cref="JsonDocument"/> into</typeparam>
        /// <param name="document">The <see cref="JsonDocument"/> to convert</param>
        /// <returns>A new object of the specified type</returns>
        public static T? ToObject<T>(this JsonDocument document)
        {
            var json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }

    }

}
