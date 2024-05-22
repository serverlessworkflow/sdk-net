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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventDefinitionBuilder"/> interface
/// </summary>
/// <param name="attributes">A name/value mapping of the event's attributes. Supports runtime expressions</param>
public class EventDefinitionBuilder(IDictionary<string, object>? attributes = null)
    : IEventDefinitionBuilder
{

    /// <summary>
    /// Gets a name/value mapping of the event's attributes
    /// </summary>
    protected virtual EquatableDictionary<string, object> Attributes { get; set; } = attributes == null ? new() : new(attributes);

    /// <inheritdoc/>
    public virtual IEventDefinitionBuilder With(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Attributes[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventDefinitionBuilder With(IDictionary<string, object> attributes)
    {
        ArgumentNullException.ThrowIfNull(attributes);
        this.Attributes = new(attributes);
        return this;
    }

    /// <inheritdoc/>
    public virtual EventDefinition Build() => new() { With = this.Attributes };

}
