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
using ServerlessWorkflow.Sdk.Models.Tasks;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="RaiseTaskDefinition"/>s
/// </summary>
public class RaiseTaskDefinitionValidator
    : AbstractValidator<RaiseTaskDefinition>
{

    /// <inheritdoc/>
    public RaiseTaskDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.RuleFor(t => t.Raise.ErrorReference!)
            .Must(ReferenceAnExistingError)
            .When(t => !string.IsNullOrWhiteSpace(t.Raise.ErrorReference));
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
    /// Determines whether or not the specified error is defined
    /// </summary>
    /// <param name="name">The name of the error to check</param>
    /// <returns>A boolean indicating whether or not the specified error is defined</returns>
    protected virtual bool ReferenceAnExistingError(string name) => this.Components?.Errors?.ContainsKey(name) == true;

}