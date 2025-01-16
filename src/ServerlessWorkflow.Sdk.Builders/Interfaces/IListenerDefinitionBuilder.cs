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
/// Defines the fundamentals of a service used to build <see cref="ListenerDefinition"/>s
/// </summary>
public interface IListenerDefinitionBuilder
    : IListenerTargetDefinitionBuilder
{

    /// <summary>
    /// Configures how to read consumed events
    /// </summary>
    /// <param name="readMode">Specifies how consumed events should be read. See <see cref="EventReadMode"/>s</param>
    /// <returns>The configured <see cref="IListenerDefinitionBuilder"/></returns>
    IListenerDefinitionBuilder Read(string readMode);

    /// <summary>
    /// Builds the configured <see cref="ListenerDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ListenerDefinition"/></returns>
    new ListenerDefinition Build();

}