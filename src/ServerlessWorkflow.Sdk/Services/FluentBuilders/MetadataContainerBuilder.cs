// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IMetadataContainerBuilder{TContainer}"/>
/// </summary>
/// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
public abstract class MetadataContainerBuilder<TContainer>
    : IMetadataContainerBuilder<TContainer>
    where TContainer : class, IMetadataContainerBuilder<TContainer>
{

    /// <inheritdoc/>
    public virtual DynamicMapping? Metadata { get; protected set; }

    /// <inheritdoc/>
    public virtual TContainer WithMetadata(string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
        this.Metadata ??= new();
        this.Metadata[key] = value;
        return (TContainer)(object)this;
    }

    /// <inheritdoc/>
    public virtual TContainer WithMetadata(IDictionary<string, object> metadata)
    {
        if(metadata == null) throw new ArgumentNullException(nameof(metadata));
        this.Metadata = new(metadata);
        return (TContainer)(object)this;
    }

}
