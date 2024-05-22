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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IJitterDefinitionBuilder"/> interface
/// </summary>
/// <param name="from">The minimum duration of the jitter range</param>
/// <param name="to">The maximum duration of the jitter range</param>
public class JitterDefinitionBuilder(Duration? from = null, Duration? to = null)
    : IJitterDefinitionBuilder
{

    /// <summary>
    /// Gets the minimum duration of the jitter range
    /// </summary>
    protected Duration? JitterFrom { get; set; } = from;

    /// <summary>
    /// Gets the maximum duration of the jitter range
    /// </summary>
    protected Duration? JitterTo { get; set; } = to;

    /// <inheritdoc/>
    public virtual IJitterDefinitionBuilder From(Duration from)
    {
        ArgumentNullException.ThrowIfNull(from);
        this.JitterFrom = from;
        return this;
    }

    /// <inheritdoc/>
    public virtual IJitterDefinitionBuilder To(Duration to)
    {
        ArgumentNullException.ThrowIfNull(to);
        this.JitterTo = to;
        return this;
    }

    /// <inheritdoc/>
    public virtual JitterDefinition Build()
    {
        if (this.JitterFrom == null) throw new NullReferenceException("The jitter range's minimum duration must be set");
        if (this.JitterTo == null) throw new NullReferenceException("The jitter range's maximum duration must be set");
        return new()
        {
            From = this.JitterFrom,
            To = this.JitterTo,
        };
    }

}
