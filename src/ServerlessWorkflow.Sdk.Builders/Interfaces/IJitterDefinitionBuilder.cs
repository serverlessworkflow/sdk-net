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
/// Defines the fundamentals of a service used to build <see cref="JitterDefinition"/>s
/// </summary>
public interface IJitterDefinitionBuilder
{

    /// <summary>
    /// Sets the jitter range's minimum duration
    /// </summary>
    /// <param name="from">The jitter range's minimum duration</param>
    /// <returns>The configured <see cref="IJitterDefinitionBuilder"/></returns>
    IJitterDefinitionBuilder From(Duration from);

    /// <summary>
    /// Sets the jitter range's maximum duration
    /// </summary>
    /// <param name="to">The jitter range's maximum duration</param>
    /// <returns>The configured <see cref="IJitterDefinitionBuilder"/></returns>
    IJitterDefinitionBuilder To(Duration to);

    /// <summary>
    /// Builds the configured <see cref="JitterDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="JitterDefinition"/></returns>
    JitterDefinition Build();

}
