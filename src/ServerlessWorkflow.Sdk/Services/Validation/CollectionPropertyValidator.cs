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
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.Services.Validation;


/// <summary>
/// Represents the service used to validate a workflow's <see cref="ICollection{T}"/>s
/// </summary>
public class CollectionPropertyValidator<TElement>
    : PropertyValidator<WorkflowDefinition, IEnumerable<TElement>?>
{

    /// <summary>
    /// Initializes a new <see cref="CollectionPropertyValidator{TElement}"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    public CollectionPropertyValidator(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public override string Name => "CollectionValidator";

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<WorkflowDefinition> context, IEnumerable<TElement>? value)
    {
        var index = 0;
        if (value == null) return true;
        foreach (TElement elem in value)
        {
            IEnumerable<IValidator<TElement>> validators = this.ServiceProvider.GetServices<IValidator<TElement>>();
            foreach (IValidator<TElement> validator in validators)
            {
                FluentValidation.Results.ValidationResult validationResult = validator.Validate(elem);
                if (validationResult.IsValid) continue;
                foreach (var failure in validationResult.Errors) context.AddFailure(failure);
                return false;
            }
            index++;
        }
        return true;
    }

}
