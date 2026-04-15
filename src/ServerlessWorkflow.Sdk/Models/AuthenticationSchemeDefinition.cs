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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the base class for all authentication scheme definitions
/// </summary>
[Description("Represents the base class for all authentication scheme definitions")]
[DataContract]
public abstract record AuthenticationSchemeDefinition
    : Extendable
{

    /// <summary>
    /// Gets the name of the authentication scheme
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public abstract string Scheme { get; }

    /// <summary>
    /// Gets/sets the name of the secret, if any, used to configure the authentication scheme
    /// </summary>
    [Description("The name of the secret, if any, used to configure the authentication scheme")]
    [DataMember(Order = 1, Name = "use"), JsonPropertyOrder(1), JsonPropertyName("use")]
    public string? Use { get; init; }

}