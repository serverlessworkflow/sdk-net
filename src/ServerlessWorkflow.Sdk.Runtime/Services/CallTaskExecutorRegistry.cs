// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a registry used to map call type discriminators (e.g. "http", "openapi") to their corresponding <see cref="ITaskExecutor"/> types
/// </summary>
public sealed class CallTaskExecutorRegistry
{

    readonly Dictionary<string, Type> registry = [];

    /// <summary>
    /// Registers the specified <see cref="ITaskExecutor{TDefinition}"/> for the specified call type
    /// </summary>
    /// <param name="callType">The call type discriminator to register the executor for (e.g. "http", "openapi", "asyncapi", "grpc")</param>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    public void Register<TExecutor>(string callType)
        where TExecutor : class, ITaskExecutor<CallTaskDefinition>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(callType);
        registry[callType] = typeof(TExecutor);
    }

    /// <summary>
    /// Resolves the <see cref="ITaskExecutor"/> type registered for the specified call type
    /// </summary>
    /// <param name="callType">The call type discriminator to resolve the executor for</param>
    /// <returns>The resolved executor type, if any</returns>
    public Type? Resolve(string callType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(callType);
        return registry.TryGetValue(callType, out var executorType) ? executorType : null;
    }

}
