/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an extension state
/// </summary>
[DiscriminatedByDefault]
[DataContract]
[ProtoContract]
public class ExtensionStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="ExtensionStateDefinition"/>
    /// </summary>
    public ExtensionStateDefinition() : base(StateType.Extension) { }

    /// <summary>
    /// Initializes a new <see cref="ExtensionStateDefinition"/>
    /// </summary>
    /// <param name="type">The type of the extension state</param>
    public ExtensionStateDefinition(string type) : base(type) { }

}
