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
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessWorkflow.Sdk.Services.Serialization
{
    /// <summary>
    /// Represents the default Newtonsoft Json implementation of the <see cref="ISerializer"/> interface
    /// </summary>
    public class NewtonsoftJsonSerializer
        : IJsonSerializer
    {

        /// <summary>
        /// Initializes a new <see cref="NewtonsoftJsonSerializer"/>
        /// </summary>
        /// <param name="settings">The service used to access the current <see cref="JsonSerializerSettings"/></param>
        public NewtonsoftJsonSerializer(IOptions<JsonSerializerSettings> settings)
        {
            this.Settings = settings.Value;
        }

        /// <summary>
        /// Gets the current <see cref="JsonSerializerSettings"/>
        /// </summary>
        protected JsonSerializerSettings Settings { get; }

        /// <inheritdoc/>
        public virtual T? Deserialize<T>(Stream input)
        {
            using StreamReader reader = new(input, leaveOpen: true);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<T?> DeserializeAsync<T>(Stream input, CancellationToken cancellationToken = default)
        {
            using StreamReader reader = new(input, leaveOpen: true);
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(json, this.Settings);
        }

        /// <inheritdoc/>
        public virtual T? Deserialize<T>(byte[] input)
        {
            string json = Encoding.UTF8.GetString(input);
            return JsonConvert.DeserializeObject<T>(json, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<T?> DeserializeAsync<T>(byte[] input, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize<T>(input), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual object? Deserialize(Stream input, Type returnType)
        {
            using StreamReader reader = new(input, leaveOpen: true);
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(json, returnType, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<object?> DeserializeAsync(Stream input, Type returnType, CancellationToken cancellationToken = default)
        {
            using StreamReader reader = new(input, leaveOpen: true);
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject(json, returnType, this.Settings);
        }

        /// <inheritdoc/>
        public virtual object? Deserialize(byte[] input, Type returnType)
        {
            string json = Encoding.UTF8.GetString(input);
            return JsonConvert.DeserializeObject(json, returnType, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<object?> DeserializeAsync(byte[] input, Type returnType, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize(input, returnType), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual object? Deserialize(string json, Type returnType)
        {
            return JsonConvert.DeserializeObject(json, returnType, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<object?> DeserializeAsync(string json, Type returnType, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize(json, returnType), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual T? Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, this.Settings);
        }

        /// <inheritdoc/>
        public virtual async Task<T?> DeserializeAsync<T>(string json, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Deserialize<T>(json), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void Serialize(object value, Stream output)
        {
            using StreamWriter writer = new(output, leaveOpen: true);
            string json = JsonConvert.SerializeObject(value, this.Settings);
            writer.Write(json);
            writer.Flush();
        }

        /// <inheritdoc/>
        public virtual async Task SerializeAsync(object value, Stream output, CancellationToken cancellationToken = default)
        {
            using StreamWriter writer = new(output, leaveOpen: true);
            string json = JsonConvert.SerializeObject(value, this.Settings);
            await writer.WriteAsync(json);
            await writer.FlushAsync();
        }

        /// <inheritdoc/>
        public virtual void Serialize(object value, Stream output, Type type)
        {
            using StreamWriter writer = new(output, leaveOpen: true);
            string json = JsonConvert.SerializeObject(value, type, this.Settings);
            writer.Write(json);
            writer.Flush();
        }

        /// <inheritdoc/>
        public virtual async Task SerializeAsync(object value, Stream output, Type type, CancellationToken cancellationToken = default)
        {
            using StreamWriter writer = new(output, leaveOpen: true);
            string json = JsonConvert.SerializeObject(value, type, this.Settings);
            await writer.WriteAsync(json);
            await writer.FlushAsync();
        }

        /// <inheritdoc/>
        public virtual byte[] Serialize(object value)
        {
            string json = JsonConvert.SerializeObject(value, this.Settings);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <inheritdoc/>
        public virtual async Task<byte[]> SerializeAsync(object value, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Serialize(value), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual byte[] Serialize(object value, Type type)
        {
            string json = JsonConvert.SerializeObject(value, type, this.Settings);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <inheritdoc/>
        public virtual async Task<byte[]> SerializeAsync(object value, Type type, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => this.Serialize(value, type), cancellationToken);
        }

    }

}
