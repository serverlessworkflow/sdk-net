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
/// Represents the default implementation of the <see cref="IWaitTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="duration">The amount of time to wait for</param>
public class WaitTaskDefinitionBuilder(Duration? duration = null)
    : TaskDefinitionBuilder<IWaitTaskDefinitionBuilder, WaitTaskDefinition>, IWaitTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the amount of time to wait for
    /// </summary>
    protected Duration? Duration { get; set; } = duration;

    /// <inheritdoc/>
    public virtual IWaitTaskDefinitionBuilder For(Duration duration)
    {
        ArgumentNullException.ThrowIfNull(duration);
        this.Duration = duration;
        return this;
    }

    /// <inheritdoc/>
    public override WaitTaskDefinition Build()
    {
        if (this.Duration == null) throw new NullReferenceException("The amount of time to wait for must be set");
        return new()
        {
            Wait = this.Duration
        };
    }

}