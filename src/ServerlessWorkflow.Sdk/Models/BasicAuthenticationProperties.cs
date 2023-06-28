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
/// Represents an object used to configure a 'Basic' authentication scheme
/// </summary>
[DataContract]
public class BasicAuthenticationProperties
    : AuthenticationProperties
{

    /// <inheritdoc/>
    public BasicAuthenticationProperties() { }

    /// <inheritdoc/>
    public BasicAuthenticationProperties(IDictionary<string, object> properties) : base(properties) { }

    /// <summary>
    /// Gets/sets the username to use when authenticating
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Username
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Username).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Username).ToCamelCase()] = value;
        }
    }

    /// <summary>
    /// Gets/sets the password to use when authenticating
    /// </summary>
    [Required, MinLength(1)]
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string Password
    {
        get
        {
            if (this.Properties.TryGetValue(nameof(Password).ToCamelCase(), out var value)) return (string)value;
            else return null!;
        }
        set
        {
            this.Properties[nameof(Password).ToCamelCase()] = value;
        }
    }

}
