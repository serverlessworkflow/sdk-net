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
/// Represents <see cref="AuthenticationProperties"/> loaded from a specific secret
/// </summary>
[DataContract]
public class SecretBasedAuthenticationProperties
    : AuthenticationProperties
{

    /// <summary>
    /// Initializes a new <see cref="SecretBasedAuthenticationProperties"/>
    /// </summary>
    public SecretBasedAuthenticationProperties() { }

    /// <summary>
    /// Initializes a new <see cref="SecretBasedAuthenticationProperties"/>
    /// </summary>
    /// <param name="secret">The name of the secret to load the <see cref="SecretBasedAuthenticationProperties"/> from</param>
    public SecretBasedAuthenticationProperties(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentNullException(nameof(secret));
        this.Secret = secret;
    }

    /// <summary>
    /// Gets the name of the secret to load the <see cref="SecretBasedAuthenticationProperties"/> from
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "secret", IsRequired = true), JsonPropertyName("secret"), YamlMember(Alias = "secret")]
    public virtual string Secret { get; set; } = null!;

}
