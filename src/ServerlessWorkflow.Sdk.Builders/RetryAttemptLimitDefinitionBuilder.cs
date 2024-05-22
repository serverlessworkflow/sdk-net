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
/// Represents the default <see cref="IRetryAttemptLimitDefinitionBuilder"/> implementation
/// </summary>
public class RetryAttemptLimitDefinitionBuilder
    : IRetryAttemptLimitDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the maximum attempts count
    /// </summary>
    protected uint? AttemptCount { get; set; }

    /// <summary>
    /// Gets/sets the duration limit, if any, for all retry attempts
    /// </summary>
    protected Duration? AttemptDuration { get; set; }

    /// <inheritdoc/>
    public virtual IRetryAttemptLimitDefinitionBuilder Count(uint count)
    {
        this.AttemptCount = count;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRetryAttemptLimitDefinitionBuilder Duration(Duration duration)
    {
        this.AttemptDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual RetryAttemptLimitDefinition Build() => new()
    {
        Count = AttemptCount,
        Duration = AttemptDuration
    };

}