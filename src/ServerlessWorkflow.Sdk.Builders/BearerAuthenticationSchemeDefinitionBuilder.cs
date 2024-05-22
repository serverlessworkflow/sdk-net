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
/// Represents the default implementation of the <see cref="IBearerAuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public class BearerAuthenticationSchemeDefinitionBuilder
    : IBearerAuthenticationSchemeDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the bearer token to use
    /// </summary>
    protected string? Token { get; set; }

    /// <inheritdoc/>
    public virtual IBearerAuthenticationSchemeDefinitionBuilder WithToken(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        this.Token = token;
        return this;
    }

    /// <inheritdoc/>
    public virtual BearerAuthenticationSchemeDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Token)) throw new NullReferenceException("The token must be set");
        return new()
        {
            Token = this.Token
        };
    }

    AuthenticationSchemeDefinition IAuthenticationSchemeDefinitionBuilder.Build() => this.Build();

}
