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

using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="AuthenticationPolicyDefinition"/>s
/// </summary>
public class AuthenticationPolicyDefinitionValidator
    : AbstractValidator<AuthenticationPolicyDefinition>
{

    /// <inheritdoc/>
    public AuthenticationPolicyDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.RuleFor(auth => auth.Use!)
            .Must(ReferenceAnExistingAuthentication)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Use));
        this.RuleFor(auth => auth.Basic!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Basic?.Use));
        this.RuleFor(auth => auth.Bearer!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Bearer?.Use));
        this.RuleFor(auth => auth.Certificate!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Certificate?.Use));
        this.RuleFor(auth => auth.Digest!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Digest?.Use));
        this.RuleFor(auth => auth.OAuth2!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.OAuth2?.Use));
        this.RuleFor(auth => auth.Oidc!.Use!)
            .Must(ReferenceAnExistingSecret)
            .When(auth => !string.IsNullOrWhiteSpace(auth.Oidc?.Use));
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the configured reusable components
    /// </summary>
    protected ComponentDefinitionCollection? Components { get; }

    /// <summary>
    /// Determines whether or not the specified authentication exists
    /// </summary>
    /// <param name="name">The name of the authentication to check</param>
    /// <returns>A boolean indicating whether or not the specified authentication exists</returns>
    protected virtual bool ReferenceAnExistingAuthentication(string name)
    {
        if (this.Components?.Authentications?.ContainsKey(name) == true) return true;
        else return false;
    }

    /// <summary>
    /// Determines whether or not the specified secret exists
    /// </summary>
    /// <param name="name">The name of the secret to check</param>
    /// <returns>A boolean indicating whether or not the specified secret exists</returns>
    protected virtual bool ReferenceAnExistingSecret(string name)
    {
        if (this.Components?.Secrets?.Contains(name) == true) return true;
        else return false;
    }

}