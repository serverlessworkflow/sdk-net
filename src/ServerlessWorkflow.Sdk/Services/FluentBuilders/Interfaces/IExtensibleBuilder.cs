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
/// Defines the fundamentals of a service used to build <see cref="IExtensible"/>s
/// </summary>
/// <typeparam name="TExtensible">The type of the <see cref="IExtensibleBuilder{TExtensible}"/></typeparam>
public interface IExtensibleBuilder<TExtensible>
     where TExtensible : class, IExtensibleBuilder<TExtensible>
{

    /// <summary>
    /// Adds the specified extension property
    /// </summary>
    /// <param name="name">The extension property name</param>
    /// <param name="value">The extension property value</param>
    /// <returns>The configured container</returns>
    TExtensible WithExtensionProperty(string name, object value);

    /// <summary>
    /// Adds the specified extension property
    /// </summary>
    /// <param name="properties">A name/value mapping of the extension properties to add</param>
    /// <returns>The configured container</returns>
    TExtensible WithExtensionProperties(IDictionary<string, object> properties);

}