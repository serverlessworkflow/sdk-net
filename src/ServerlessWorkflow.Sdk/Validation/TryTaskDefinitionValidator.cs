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
/// Represents the <see cref="IValidator"/> used to validate <see cref="TryTaskDefinition"/>s
/// </summary>
public class TryTaskDefinitionValidator
    : AbstractValidator<TryTaskDefinition>
{

    /// <inheritdoc/>
    public TryTaskDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.RuleForEach(t => t.Try)
            .SetValidator(t => new TaskMapEntryValidator(this.ServiceProvider, this.Components, t.Try.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
        this.RuleForEach(t => t.Catch.Do)
            .SetValidator(t => new TaskMapEntryValidator(this.ServiceProvider, this.Components, t.Catch.Do?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)))
            .When(t => t.Catch.Do != null);
        this.RuleFor(t => t.Catch.RetryReference!)
            .Must(ReferenceAnExistingRetryPolicy)
            .When(t => !string.IsNullOrWhiteSpace(t.Catch.RetryReference));
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
    /// Determines whether or not the specified retry policy is defined
    /// </summary>
    /// <param name="name">The name of the retry policy to check</param>
    /// <returns>A boolean indicating whether or not the specified retry policy is defined</returns>
    protected virtual bool ReferenceAnExistingRetryPolicy(string name) => this.Components?.Retries?.ContainsKey(name) == true;

}