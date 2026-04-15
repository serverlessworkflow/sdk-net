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
/// Represents the default implementation of the <see cref="ISchemaHandlerProvider"/> interface
/// </summary>
/// <param name="handlers">An <see cref="IEnumerable{T}"/> containing all registered <see cref="ISchemaHandler"/>s</param>
public sealed class SchemaHandlerProvider(IEnumerable<ISchemaHandler> handlers)
    : ISchemaHandlerProvider
{

    /// <inheritdoc/>
    public ISchemaHandler? GetHandler(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        format = format.Trim();
        return handlers.FirstOrDefault(h => h.Supports(format));
    }

}
