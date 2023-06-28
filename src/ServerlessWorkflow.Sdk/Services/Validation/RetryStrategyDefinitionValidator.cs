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
/// Represents the service used to validate <see cref="RetryDefinition"/>s
/// </summary>
public class RetryStrategyDefinitionValidator
    : AbstractValidator<RetryDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="RetryStrategyDefinitionValidator"/>
    /// </summary>
    public RetryStrategyDefinitionValidator()
    {
        this.RuleFor(r => r.Name)
            .NotEmpty()
            .WithErrorCode($"{nameof(RetryDefinition)}.{nameof(RetryDefinition.Name)}");
    }

}
