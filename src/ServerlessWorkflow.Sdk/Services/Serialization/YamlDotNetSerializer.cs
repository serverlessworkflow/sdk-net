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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IYamlDotNetSerializer = YamlDotNet.Serialization.ISerializer;
using IYamlDotNetDeserializer = YamlDotNet.Serialization.IDeserializer;

namespace ServerlessWorkflow.Sdk.Services.Serialization
{
    /// <summary>
    /// Represents the default <see href="https://github.com/aaubry/YamlDotNet">YamlDotNet</see> implementation of the <see cref="ISerializer"/> interface
    /// </summary>
    public class YamlDotNetSerializer
        : ISerializer, IYamlSerializer
    {

        /// <summary>
        /// Initializes a new <see cref="YamlDotNetSerializer"/>
        /// </summary>
        /// <param name="serializer">The underlying <see cref="IYamlDotNetSerializer"/></param>
        /// <param name="deserializer">The underlying <see cref="IYamlDotNetDeserializer"/></param>
        public YamlDotNetSerializer(IYamlDotNetSerializer serializer, IYamlDotNetDeserializer deserializer)
        {
            this.Serializer = serializer;
            this.Deserializer = deserializer;
        }

        /// <summary>
        /// Gets the underlying <see cref="IYamlDotNetSerializer"/>
        /// </summary>
        protected IYamlDotNetSerializer Serializer { get; }

        /// <summary>
        /// Gets the underlying <see cref="IYamlDotNetDeserializer"/>
        /// </summary>
        protected IYamlDotNetDeserializer Deserializer { get; }

        /// <inheritdoc/>
        public virtual T Deserialize<T>(Stream input)
        {
            using (StreamReader reader = new StreamReader(input, leaveOpen: true))
            {
                string yaml = reader.ReadToEnd();
                return this.Deserializer.Deserialize<T>(yaml);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<T> DeserializeAsync<T>(Stream input, CancellationToken cancellationToken = default)
        {
            using (StreamReader reader = new StreamReader(input, leaveOpen: true))
            {
                string yaml = await reader.ReadToEndAsync();
                return this.Deserializer.Deserialize<T>(yaml);
            }
        }

        /// <inheritdoc/>
        public virtual T Deserialize<T>(byte[] input)
        {
            string yaml = Encoding.UTF8.GetString(input);
            return this.Deserializer.Deserialize<T>(yaml);
        }

        /// <inheritdoc/>
        public virtual async Task<T> DeserializeAsync<T>(byte[] input, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize<T>(input), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual object Deserialize(Stream input, Type returnType)
        {
            using (StreamReader reader = new StreamReader(input, leaveOpen: true))
            {
                string yaml = reader.ReadToEnd();
                return this.Deserializer.Deserialize(yaml, returnType);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeserializeAsync(Stream input, Type returnType, CancellationToken cancellationToken = default)
        {
            using (StreamReader reader = new StreamReader(input, leaveOpen: true))
            {
                string yaml = await reader.ReadToEndAsync();
                return this.Deserializer.Deserialize(yaml, returnType);
            }
        }

        /// <inheritdoc/>
        public virtual object Deserialize(byte[] input, Type returnType)
        {
            string yaml = Encoding.UTF8.GetString(input);
            return this.Deserializer.Deserialize(yaml, returnType);
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeserializeAsync(byte[] input, Type returnType, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize(input, returnType), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual object Deserialize(string yaml, Type returnType)
        {
            return this.Deserializer.Deserialize(yaml, returnType);
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeserializeAsync(string yaml, Type returnType, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize(yaml, returnType), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual T Deserialize<T>(string yaml)
        {
            return this.Deserializer.Deserialize<T>(yaml);
        }

        /// <inheritdoc/>
        public virtual async Task<T> DeserializeAsync<T>(string yaml, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize<T>(yaml), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void Serialize(object value, Stream output)
        {
            using (StreamWriter writer = new StreamWriter(output, leaveOpen: true))
            {
                string yaml = this.Serializer.Serialize(value);
                writer.Write(yaml);
                writer.Flush();
            }
        }

        /// <inheritdoc/>
        public virtual async Task SerializeAsync(object value, Stream output, CancellationToken cancellationToken = default)
        {
            using (StreamWriter writer = new StreamWriter(output, leaveOpen: true))
            {
                string yaml = this.Serializer.Serialize(value);
                await writer.WriteAsync(yaml);
                await writer.FlushAsync();
            }
        }

        /// <inheritdoc/>
        public virtual void Serialize(object value, Stream output, Type type)
        {
            using (StreamWriter writer = new StreamWriter(output, leaveOpen: true))
            {
                this.Serializer.Serialize(writer, value, type);
                writer.Flush();
            }
        }

        /// <inheritdoc/>
        public virtual async Task SerializeAsync(object value, Stream output, Type type, CancellationToken cancellationToken = default)
        {
            using (StreamWriter writer = new StreamWriter(output, leaveOpen: true))
            {
                this.Serializer.Serialize(writer, value, type);
                await writer.FlushAsync();
            }
        }

        /// <inheritdoc/>
        public virtual byte[] Serialize(object value)
        {
            string yaml = this.Serializer.Serialize(value);
            return Encoding.UTF8.GetBytes(yaml);
        }

        /// <inheritdoc/>
        public virtual async Task<byte[]> SerializeAsync(object value, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Serialize(value), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual byte[] Serialize(object value, Type type)
        {
            StringWriter writer = new StringWriter();
            this.Serializer.Serialize(writer, value, type);
            return Encoding.UTF8.GetBytes(writer.ToString());
        }

        /// <inheritdoc/>
        public virtual async Task<byte[]> SerializeAsync(object value, Type type, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Serialize(value, type), cancellationToken);
        }

    }

}
