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
/// Represents the base class for all <see cref="IAuthenticationDefinitionBuilder"/> implementations
/// </summary>
public abstract class AuthenticationDefinitionBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Initializes a new <see cref="AuthenticationDefinitionBuilder"/>
    /// </summary>
    /// <param name="authenticationDefinition">The <see cref="Models.AuthenticationDefinition"/> to configure</param>
    protected AuthenticationDefinitionBuilder(AuthenticationDefinition authenticationDefinition)
    {
        this.AuthenticationDefinition = authenticationDefinition ?? throw new ArgumentNullException(nameof(authenticationDefinition));
    }

    /// <summary>
    /// Gets the <see cref="Models.AuthenticationDefinition"/> to configure
    /// </summary>
    protected AuthenticationDefinition AuthenticationDefinition { get; }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.AuthenticationDefinition.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithExtensionProperty(string name, object value)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.AuthenticationDefinition.ExtensionData ??= new Dictionary<string, object>();
        this.AuthenticationDefinition.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IAuthenticationDefinitionBuilder WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.AuthenticationDefinition.ExtensionData = properties ?? throw new ArgumentNullException(nameof(properties));
        return this;
    }

    /// <inheritdoc/>
    public virtual void LoadFromSecret(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentNullException(nameof(secret));
        this.AuthenticationDefinition.Properties = new SecretBasedAuthenticationProperties(secret);
    }

    /// <inheritdoc/>
    public virtual AuthenticationDefinition Build() => this.AuthenticationDefinition;

}
