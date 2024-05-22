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
/// Represents the default implementation of the <see cref="IRetryPolicyLimitDefinitionBuilder"/> interface
/// </summary>
public class RetryPolicyLimitDefinitionBuilder
    : IRetryPolicyLimitDefinitionBuilder
{

    /// <summary>
    /// Gets the service used to build the definition of the limits for all retry attempts of a given policy
    /// </summary>
    protected IRetryAttemptLimitDefinitionBuilder? LimitAttempt { get; set; }

    /// <summary>
    /// Gets the maximum duration during which retrying is allowed 
    /// </summary>
    protected Duration? LimitDuration { get; set; }

    /// <inheritdoc/>
    public virtual IRetryAttemptLimitDefinitionBuilder Attempt()
    {
        this.LimitAttempt = new RetryAttemptLimitDefinitionBuilder();
        return this.LimitAttempt;
    }

    /// <inheritdoc/>
    public virtual IRetryPolicyLimitDefinitionBuilder Duration(Duration duration)
    {
        this.LimitDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual RetryPolicyLimitDefinition Build() => new()
    {
        Attempt = this.LimitAttempt?.Build(),
        Duration = this.LimitDuration,
    };

}