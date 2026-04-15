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
public sealed class RetryPolicyLimitDefinitionBuilder
    : IRetryPolicyLimitDefinitionBuilder
{

    RetryAttemptLimitDefinitionBuilder? limitAttempt;
    Duration? limitDuration;

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Attempt()
    {
        limitAttempt = new RetryAttemptLimitDefinitionBuilder();
        return limitAttempt;
    }

    /// <inheritdoc/>
    public IRetryPolicyLimitDefinitionBuilder Duration(Duration duration)
    {
        limitDuration = duration;
        return this;
    }

    /// <inheritdoc/>
    public RetryPolicyLimitDefinition Build() => new()
    {
        Attempt = limitAttempt?.Build(),
        Duration = limitDuration,
    };

}
