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
/// Defines the fundamentals of a service used to build <see cref="RetryAttemptLimitDefinition"/>s
/// </summary>
public interface IRetryAttemptLimitDefinitionBuilder
{

    /// <summary>
    /// Sets the maximum attempts count
    /// </summary>
    /// <param name="count">The maximum attempts count</param>
    /// <returns>The configured <see cref="IRetryAttemptLimitDefinitionBuilder"/></returns>
    IRetryAttemptLimitDefinitionBuilder Count(uint count);

    /// <summary>
    /// Sets the maximum duration per attempt
    /// </summary>
    /// <param name="duration">The maximum duration per attempt</param>
    /// <returns>The configured <see cref="IRetryPolicyLimitDefinitionBuilder"/></returns>
    IRetryAttemptLimitDefinitionBuilder Duration(Duration duration);

    /// <summary>
    /// Builds the configured <see cref="RetryAttemptLimitDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="RetryAttemptLimitDefinition"/></returns>
    RetryAttemptLimitDefinition Build();

}
