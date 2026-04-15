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
public sealed class OAuth2AuthenticationRequestDefinitionBuilder
    : IOAuth2AuthenticationRequestDefinitionBuilder
{

    string? encoding;

    /// <inheritdoc/>
    public IOAuth2AuthenticationRequestDefinitionBuilder WithEncoding(string encoding)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encoding);
        this.encoding = encoding;
        return this;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(encoding)) throw new NullReferenceException("The request encoding must be set");
        return new() 
        { 
            Encoding = encoding 
        };
    }

}
