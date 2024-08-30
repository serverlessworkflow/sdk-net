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
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationRequestDefinitionBuilder"/> interface
/// </summary>
public class OAuth2AuthenticationRequestDefinitionBuilder
    : IOAuth2AuthenticationRequestDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'
    /// </summary>
    public virtual string? Encoding { get; set; }

    /// <inheritdoc/>
    public virtual IOAuth2AuthenticationRequestDefinitionBuilder WithEncoding(string encoding)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encoding);
        this.Encoding = encoding;
        return this;
    }

    /// <inheritdoc/>
    public virtual OAuth2AuthenticationRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Encoding)) throw new NullReferenceException("The request encoding must be set");
        return new() { Encoding = this.Encoding };
    }

}