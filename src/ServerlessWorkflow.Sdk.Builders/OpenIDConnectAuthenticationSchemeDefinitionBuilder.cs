﻿// Copyright © 2024-Present The Serverless Workflow Specification Authors
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
/// Represents the default implementation of the <see cref="IOpenIDConnectAuthenticationSchemeDefinitionBuilder"/> interface
/// </summary>
public class OpenIDConnectAuthenticationSchemeDefinitionBuilder
    : OAuth2AuthenticationSchemeDefinitionBuilder<OpenIDConnectSchemeDefinition, IOpenIDConnectAuthenticationSchemeDefinitionBuilder>, IOpenIDConnectAuthenticationSchemeDefinitionBuilder
{

    /// <inheritdoc/>
    public override OpenIDConnectSchemeDefinition Build()
    {
        if (this.Authority == null) throw new NullReferenceException("The authority must be set");
        if (string.IsNullOrWhiteSpace(this.GrantType)) throw new NullReferenceException("The grant type must be set");
        return new()
        {
            Use = this.Secret,
            Authority = this.Authority,
            Grant = this.GrantType,
            Client = this.Client,
            Request = this.Request,
            Issuers = this.Issuers,
            Audiences = this.Audiences,
            Scopes = this.Scopes,
            Actor = this.Actor,
            Username = this.Username,
            Password = this.Password,
            Subject = this.Subject
        };
    }

}