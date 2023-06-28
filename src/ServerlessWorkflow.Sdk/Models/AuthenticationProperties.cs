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
/// Represents an object used to configure an authentication mechanism
/// </summary>
[DataContract, KnownType(nameof(GetKnownTypes))]
public abstract class AuthenticationProperties
{

    /// <summary>
    /// Initializes a new <see cref="AuthenticationProperties"/>
    /// </summary>
    protected AuthenticationProperties() 
    {
        this.Properties = new Dictionary<string, object>();
    }

    /// <summary>
    /// Initializes a new <see cref="AuthenticationProperties"/>
    /// </summary>
    /// <param name="properties">A key/value mapping of the authentication properties to wrap</param>
    protected AuthenticationProperties(IDictionary<string, object> properties)
    {
        this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
    }

    /// <summary>
    /// Gets/sets a key/value mapping of the authentication properties to wrap
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual IDictionary<string, object> Properties { get; set; }

    static Type[] GetKnownTypes()
    {
        return new Type[]
        {
            typeof(BasicAuthenticationProperties),
            typeof(BearerAuthenticationProperties),
            typeof(OAuth2AuthenticationProperties),
            typeof(SecretBasedAuthenticationProperties)
        };
    }

}
