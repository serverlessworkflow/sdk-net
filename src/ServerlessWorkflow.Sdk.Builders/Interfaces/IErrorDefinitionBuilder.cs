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
/// Defines the fundamentals of a service used to build <see cref="ErrorDefinition"/>s
/// </summary>
public interface IErrorDefinitionBuilder
{

    /// <summary>
    /// Sets the error's type
    /// </summary>
    /// <param name="type">The type of the error to build. Supports runtime expressions</param>
    /// <returns>The configures <see cref="IErrorDefinitionBuilder"/></returns>
    IErrorDefinitionBuilder WithType(string type);

    /// <summary>
    /// Sets the error's status
    /// </summary>
    /// <param name="status">The status of the error to build. Supports runtime expressions</param>
    /// <returns>The configures <see cref="IErrorDefinitionBuilder"/></returns>
    IErrorDefinitionBuilder WithStatus(string status);

    /// <summary>
    /// Sets the error's title
    /// </summary>
    /// <param name="title">The type of the error to build. Supports runtime expressions</param>
    /// <returns>The configures <see cref="IErrorDefinitionBuilder"/></returns>
    IErrorDefinitionBuilder WithTitle(string title);

    /// <summary>
    /// Sets the error's detail
    /// </summary>
    /// <param name="detail">The detail of the error to build. Supports runtime expressions</param>
    /// <returns>The configures <see cref="IErrorDefinitionBuilder"/></returns>
    IErrorDefinitionBuilder WithDetail(string detail);

    /// <summary>
    /// Sets a reference to the component the error concerns
    /// </summary>
    /// <param name="instance">The instance of the error to build. Supports runtime expressions</param>
    /// <returns>The configures <see cref="IErrorDefinitionBuilder"/></returns>
    IErrorDefinitionBuilder WithInstance(string instance);

    /// <summary>
    /// Builds the configured <see cref="ErrorDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ErrorDefinition"/></returns>
    ErrorDefinition Build();

}
