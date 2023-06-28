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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to configure a 'Bearer' authentication scheme
/// </summary>

[DataContract]
public class BearerAuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public BearerAuthenticationProperties() { }

    /// <inheritdoc/>
    public BearerAuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

    /// <summary>
    /// Gets/sets the token used to authenticate
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Token
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Token).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Token).ToCamelCase()] = value;
        }
    }

}
