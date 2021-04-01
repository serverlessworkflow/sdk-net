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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.Serialization
{
    /// <summary>
    /// Defines the fundamentals of a service used to serialize/deserialize objects
    /// </summary>
    public interface ISerializer
    {

        /// <summary>
        /// Serializes a value to an output <see cref="Stream"/>
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="output">The output <see cref="Stream"/> to serialize the specified value to</param>
        void Serialize(object value, Stream output);

        /// <summary>
        /// Serializes a value to an output <see cref="Stream"/>
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="output">The output <see cref="Stream"/> to serialize the specified value to</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task SerializeAsync(object value, Stream output, CancellationToken cancellationToken = default);

        /// <summary>
        /// Serializes a value to an output <see cref="Stream"/>
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="output">The output <see cref="Stream"/> to serialize the specified value to</param>
        /// <param name="type">The type of the value to serialize</param>
        void Serialize(object value, Stream output, Type type);

        /// <summary>
        /// Serializes a value to an output <see cref="Stream"/>
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="output">The output <see cref="Stream"/> to serialize the specified value to</param>
        /// <param name="type">The type of the value to serialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task SerializeAsync(object value, Stream output, Type type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Serializes a value to a byte array
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <returns>A new byte array representing the serialized value</returns>
        byte[] Serialize(object value);

        /// <summary>
        /// Serializes a value to a byte array
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new byte array representing the serialized value</returns>
        Task<byte[]> SerializeAsync(object value, CancellationToken cancellationToken = default);

        /// <summary>
        /// Serializes a value to a byte array
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="type">The type of the value to serialize</param>
        /// <returns>A new byte array representing the serialized value</returns>
        byte[] Serialize(object value, Type type);

        /// <summary>
        /// Serializes a value to a byte array
        /// </summary>
        /// <param name="value">The value to serialize</param>
        /// <param name="type">The type of the value to serialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new byte array representing the serialized value</returns>
        Task<byte[]> SerializeAsync(object value, Type type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserialize a value from an input <see cref="Stream"/>
        /// </summary>
        /// <typeparam name="T">The type of the value to deserialize</typeparam>
        /// <param name="input">The input <see cref="Stream"/> to deserialize the value from</param>
        /// <returns>The deserialized value</returns>
        T Deserialize<T>(Stream input);

        /// <summary>
        /// Deserialize a value from an input <see cref="Stream"/>
        /// </summary>
        /// <typeparam name="T">The type of the value to deserialize</typeparam>
        /// <param name="input">The input <see cref="Stream"/> to deserialize the value from</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<T> DeserializeAsync<T>(Stream input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserialize a value from an input byte array
        /// </summary>
        /// <typeparam name="T">The type of the value to deserialize</typeparam>
        /// <param name="input">The input byte array to deserialize the value from</param>
        /// <returns>The deserialized value</returns>
        T Deserialize<T>(byte[] input);

        /// <summary>
        /// Deserialize a value from an input byte array
        /// </summary>
        /// <typeparam name="T">The type of the value to deserialize</typeparam>
        /// <param name="input">The input byte array to deserialize the value from</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<T> DeserializeAsync<T>(byte[] input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserialize a value from an input <see cref="Stream"/>
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/> to deserialize the value from</param>
        /// <param name="returnType">The type of the value to deserialize</param>
        /// <returns>The deserialized value</returns>
        object Deserialize(Stream input, Type returnType);

        /// <summary>
        /// Deserialize a value from an input <see cref="Stream"/>
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/> to deserialize the value from</param>
        /// <param name="returnType">The type of the value to deserialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<object> DeserializeAsync(Stream input, Type returnType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserialize a value from an input byte array
        /// </summary>
        /// <param name="input">The input byte array to deserialize the value from</param>
        /// <param name="returnType">The type of the value to deserialize</param>
        /// <returns>The deserialized value</returns>
        object Deserialize(byte[] input, Type returnType);

        /// <summary>
        /// Deserialize a value from an input byte array
        /// </summary>
        /// <param name="input">The input byte array to deserialize the value from</param>
        /// <param name="returnType">The type of the value to deserialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>The deserialized value</returns>
        Task<object> DeserializeAsync(byte[] input, Type returnType, CancellationToken cancellationToken = default);

    }

}
