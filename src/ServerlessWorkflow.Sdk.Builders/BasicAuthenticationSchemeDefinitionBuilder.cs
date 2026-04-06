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
public sealed class BasicAuthenticationSchemeDefinitionBuilder
    : AuthenticationSchemeDefinitionBuilder<BasicAuthenticationSchemeDefinition>, IBasicAuthenticationSchemeDefinitionBuilder
{

    string? username;
    string? password;

    /// <inheritdoc/>
    public IBasicAuthenticationSchemeDefinitionBuilder WithUsername(string username)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        this.username = username;
        return this;
    }

    /// <inheritdoc/>
    public IBasicAuthenticationSchemeDefinitionBuilder WithPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        this.password = password;
        return this;
    }

    /// <inheritdoc/>
    public override BasicAuthenticationSchemeDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(username)) throw new NullReferenceException("The username must be set");
        if (string.IsNullOrWhiteSpace(password)) throw new NullReferenceException("The password must be set");
        return new()
        {
            Use = Secret,
            Username = username,
            Password = password
        };
    }

    AuthenticationSchemeDefinition IAuthenticationSchemeDefinitionBuilder.Build() => Build();

}
