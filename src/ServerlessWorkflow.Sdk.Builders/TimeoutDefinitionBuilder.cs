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

using System.Xml;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ITimeoutDefinitionBuilder"/> interface
/// </summary>
public class TimeoutDefinitionBuilder
    : ITimeoutDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the duration after which to timeout
    /// </summary>
    protected Duration? AfterValue { get; set; }

    /// <inheritdoc/>
    public virtual ITimeoutDefinitionBuilder After(string duration)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(duration);
        this.AfterValue = XmlConvert.ToTimeSpan(duration);
        return this;
    }

    /// <inheritdoc/>
    public virtual ITimeoutDefinitionBuilder After(Duration duration)
    {
        ArgumentNullException.ThrowIfNull(duration);
        this.AfterValue = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual TimeoutDefinition Build()
    {
        if (this.AfterValue == null) throw new NullReferenceException("The duration after which to timeout must be set");
        return new()
        {
            After = this.AfterValue
        };
    }

}