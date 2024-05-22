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
/// Defines the fundamentals of a service used to build <see cref="EventDefinition"/>s
/// </summary>
public interface IEventDefinitionBuilder
{

    /// <summary>
    /// Adds a new attribute to the event
    /// </summary>
    /// <param name="name">The attribute's name</param>
    /// <param name="value">The attribute's value. Supports runtime expressions</param>
    /// <returns>The configured <see cref="IEventDefinitionBuilder"/></returns>
    IEventDefinitionBuilder With(string name, object value);

    /// <summary>
    /// Sets the event's attributes
    /// </summary>
    /// <param name="attributes">A name/value mapping of the event's attributes. Supports runtime expressions</param>
    /// <returns>The configured <see cref="IEventDefinitionBuilder"/></returns>
    IEventDefinitionBuilder With(IDictionary<string, object> attributes);

    /// <summary>
    /// Builds the configured <see cref="EventDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="EventDefinition"/></returns>
    EventDefinition Build();

}
