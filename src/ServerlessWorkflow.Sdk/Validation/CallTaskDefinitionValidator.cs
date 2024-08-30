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
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.Serialization;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Models.Calls;
using ServerlessWorkflow.Sdk.Models.Tasks;
using ServerlessWorkflow.Sdk.Properties;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="CallTaskDefinition"/>s
/// </summary>
public class CallTaskDefinitionValidator
    : AbstractValidator<CallTaskDefinition>
{

    /// <inheritdoc/>
    public CallTaskDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.RuleFor(c => c.Call)
            .Must(ReferenceAnExistingFunction)
            .When(c => !Uri.TryCreate(c.Call, UriKind.Absolute, out _))
            .WithMessage(ValidationErrors.UndefinedFunction);
        this.When(c => c.Call == Function.AsyncApi, () =>
        {
            this.RuleFor(c => (AsyncApiCallDefinition)this.JsonSerializer.Convert(c.With, typeof(AsyncApiCallDefinition))!)
                .SetValidator(c => new AsyncApiCallDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(c => c.Call == Function.Grpc, () =>
        {
            this.RuleFor(c => (GrpcCallDefinition)this.JsonSerializer.Convert(c.With, typeof(GrpcCallDefinition))!)
                .SetValidator(c => new GrpcCallDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(c => c.Call == Function.Http, () =>
        {
            this.RuleFor(c => (HttpCallDefinition)this.JsonSerializer.Convert(c.With, typeof(HttpCallDefinition))!)
                .SetValidator(c => new HttpCallDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(c => c.Call == Function.OpenApi, () =>
        {
            this.RuleFor(c => (OpenApiCallDefinition)this.JsonSerializer.Convert(c.With, typeof(OpenApiCallDefinition))!)
                .SetValidator(c => new OpenApiCallDefinitionValidator(this.ServiceProvider, this.Components));
        });
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the service used to serialize/deserialize data to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer => this.ServiceProvider.GetRequiredService<IJsonSerializer>();

    /// <summary>
    /// Gets the configured reusable components
    /// </summary>
    protected ComponentDefinitionCollection? Components { get; }

    /// <summary>
    /// Determines whether or not the specified function exists
    /// </summary>
    /// <param name="name">The name of the function to check</param>
    /// <returns>A boolean indicating whether or not the specified function exists</returns>
    protected virtual bool ReferenceAnExistingFunction(string name)
    {
        if (Function.AsEnumerable().Contains(name)) return true;
        else if (this.Components?.Functions?.ContainsKey(name) == true) return true;
        else return false;
    }

}
