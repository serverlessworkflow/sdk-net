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
/// Defines the fundamentals of a service used to build <see cref="RetryPolicyLimitDefinition"/>s
/// </summary>
public interface IRetryPolicyLimitDefinitionBuilder
{

    /// <summary>
    /// Configures retry attempts limits
    /// </summary>
    /// <returns>A new <see cref="IRetryAttemptLimitDefinitionBuilder"/></returns>
    IRetryAttemptLimitDefinitionBuilder Attempt();

    /// <summary>
    /// Configures the maximum duration during which retrying is allowed
    /// </summary>
    /// <param name="duration">The maximum duration during which retrying is allowed</param>
    /// <returns>The configured <see cref="IRetryPolicyLimitDefinitionBuilder"/></returns>
    IRetryPolicyLimitDefinitionBuilder Duration(Duration duration);

    /// <summary>
    /// Builds the configured <see cref="RetryPolicyLimitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="RetryPolicyLimitDefinition"/></returns>
    RetryPolicyLimitDefinition Build();

}
