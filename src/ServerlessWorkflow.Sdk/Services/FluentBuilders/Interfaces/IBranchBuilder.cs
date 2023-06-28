// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to configure <see cref="BranchDefinition"/>s
/// </summary>
public interface IBranchBuilder
    : IActionCollectionBuilder<IBranchBuilder>, IExtensibleBuilder<IBranchBuilder>
{

    /// <summary>
    /// Sets the <see cref="BranchDefinition"/>'s name
    /// </summary>
    /// <param name="name">The <see cref="BranchDefinition"/>'s name</param>
    /// <returns>The configured <see cref="IBranchBuilder"/></returns>
    IBranchBuilder WithName(string name);

    /// <summary>
    /// Builds the <see cref="BranchDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="BranchDefinition"/></returns>
    BranchDefinition Build();

}
