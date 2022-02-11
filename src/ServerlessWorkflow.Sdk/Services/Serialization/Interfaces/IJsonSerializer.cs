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
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.Serialization
{

    /// <summary>
    /// Defines the fundamentals of a service used to serialize and deserialize JSON
    /// </summary>
    public interface IJsonSerializer
        : ISerializer
    {

        /// <summary>
        /// Deserializes the specified JSON string
        /// </summary>
        /// <param name="json">The JSON string to deserialize</param>
        /// <param name="returnType">The expected return type</param>
        /// <returns>The deserialized value</returns>
        object? Deserialize(string json, Type returnType);

        /// <summary>
        /// Deserializes the specified JSON string
        /// </summary>
        /// <param name="json">The JSON string to deserialize</param>
        /// <param name="returnType">The expected return type</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<object?> DeserializeAsync(string json, Type returnType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserializes the specified JSON string
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>The deserialized value</returns>
        T? Deserialize<T>(string json);

        /// <summary>
        /// Deserializes the specified JSON string
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<T?> DeserializeAsync<T>(string json, CancellationToken cancellationToken = default);

    }

}
