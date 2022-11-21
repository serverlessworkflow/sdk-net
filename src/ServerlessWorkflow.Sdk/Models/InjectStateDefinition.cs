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
/// Represents a workflow state that injects static data into state data input
/// </summary>
[DiscriminatorValue(StateType.Inject)]
[DataContract]
[ProtoContract]
public class InjectStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="InjectStateDefinition"/>
    /// </summary>
    public InjectStateDefinition()
        : base(StateType.Inject)
    {

    }

    /// <summary>
    /// Gets/sets the object to inject within the state's data input and can be manipulated via filter
    /// </summary>
    [Required]
    [Newtonsoft.Json.JsonRequired]
    [ProtoMember(1, IsRequired = true)]
    [DataMember(Order = 1, IsRequired = true)]
    public virtual DynamicObject Data { get; set; } = null!;

}
