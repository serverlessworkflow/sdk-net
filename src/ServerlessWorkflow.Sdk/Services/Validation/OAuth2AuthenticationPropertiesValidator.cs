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

using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="OAuth2AuthenticationProperties"/>s
/// </summary>
public class OAuth2AuthenticationPropertiesValidator
    : AbstractValidator<OAuth2AuthenticationProperties>
{

    /// <summary>
    /// Initializes a new <see cref="OAuth2AuthenticationPropertiesValidator"/>
    /// </summary>
    public OAuth2AuthenticationPropertiesValidator()
    {
        this.RuleFor(a => a.Authority)
            .NotNull();
        this.RuleFor(a => a.ClientId)
            .NotEmpty();
        this.RuleFor(a => a.Username)
            .NotEmpty()
            .When(a => a.GrantType == OAuth2GrantType.Password);
        this.RuleFor(a => a.Password)
            .NotEmpty()
            .When(a => a.GrantType == OAuth2GrantType.Password);
    }

}
