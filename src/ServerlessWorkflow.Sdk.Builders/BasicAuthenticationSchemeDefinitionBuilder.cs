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
/// Represents the default implementation of the <see cref="IBasicAuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public class BasicAuthenticationSchemeDefinitionBuilder
    : IBasicAuthenticationSchemeDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the username to use
    /// </summary>
    protected string? Username { get; set; }

    /// <summary>
    /// Gets/sets the password to use
    /// </summary>
    protected string? Password { get; set; }

    /// <inheritdoc/>
    public virtual IBasicAuthenticationSchemeDefinitionBuilder WithUsername(string username)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        this.Username = username;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBasicAuthenticationSchemeDefinitionBuilder WithPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        this.Password = password;
        return this;
    }

    /// <inheritdoc/>
    public virtual BasicAuthenticationSchemeDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Username)) throw new NullReferenceException("The username must be set");
        if (string.IsNullOrWhiteSpace(this.Password)) throw new NullReferenceException("The password must be set");
        return new()
        {
            Username = this.Username,
            Password = this.Password
        };
    }

    AuthenticationSchemeDefinition IAuthenticationSchemeDefinitionBuilder.Build() => this.Build();

}
