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
using ServerlessWorkflow.Sdk.Properties;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="ErrorCatcherDefinition"/>s
/// </summary>
public class ErrorCatcherDefinitionValidator
    : AbstractValidator<ErrorCatcherDefinition>
{

    /// <inheritdoc/>
    public ErrorCatcherDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.RuleFor(e => e)
            .Must(HaveValidHandlers)
            .WithMessage("The catch node must define either a 'do' section or a retry policy");
        this.RuleForEach(e => e.Do)
            .SetValidator(e => new TaskMapEntryValidator(this.ServiceProvider, this.Components, e.Do?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)))
            .When(e => e.Do != null);
        this.RuleFor(e => e.RetryReference!)
            .Must(ReferenceAnExistingRetryPolicy)
            .When(e => !string.IsNullOrWhiteSpace(e.RetryReference));
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
    /// Determines whether the error catcher has valid handlers (either 'do' tasks or a retry policy)
    /// </summary>
    /// <param name="catcher">The error catcher to validate</param>
    /// <returns>A boolean indicating whether the error catcher has valid handlers</returns>
    protected virtual bool HaveValidHandlers(ErrorCatcherDefinition catcher)
    {
        return (catcher.Do != null && catcher.Do.Count > 0) || catcher.Retry != null || !string.IsNullOrWhiteSpace(catcher.RetryReference);
    }

    /// <summary>
    /// Determines whether or not the specified retry policy is defined
    /// </summary>
    /// <param name="name">The name of the retry policy to check</param>
    /// <returns>A boolean indicating whether or not the specified retry policy is defined</returns>
    protected virtual bool ReferenceAnExistingRetryPolicy(string name) => this.Components?.Retries?.ContainsKey(name) == true;
}
