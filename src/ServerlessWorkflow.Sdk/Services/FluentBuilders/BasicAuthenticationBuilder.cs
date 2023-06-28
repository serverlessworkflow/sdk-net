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
/// Represents the default implementation of the <see cref="IBasicAuthenticationBuilder"/>
/// </summary>
public class BasicAuthenticationBuilder
    : AuthenticationDefinitionBuilder, IBasicAuthenticationBuilder
{

    /// <summary>
    /// Initializes a new <see cref="BasicAuthenticationBuilder"/>
    /// </summary>
    public BasicAuthenticationBuilder() : base(new AuthenticationDefinition() { Scheme = AuthenticationScheme.Basic, Properties = new BasicAuthenticationProperties() }) { }

    /// <summary>
    /// Gets the <see cref="BasicAuthenticationProperties"/> of the authentication definition to build
    /// </summary>
    protected BasicAuthenticationProperties Properties => (BasicAuthenticationProperties)this.AuthenticationDefinition.Properties!;

    /// <inheritdoc/>
    public virtual IBasicAuthenticationBuilder WithUserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
        this.Properties.Username = username;
        return this;
    }

    /// <inheritdoc/>
    public virtual IBasicAuthenticationBuilder WithPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
        this.Properties.Password = password;
        return this;
    }

}
