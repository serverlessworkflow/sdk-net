﻿// Copyright © 2024-Present The Serverless Workflow Specification Authors
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
/// Represents the <see cref="IValidator"/> used to validate <see cref="CatalogDefinition"/>s
/// </summary>
public class CatalogDefinitionValidator
    : AbstractValidator<CatalogDefinition>
{

    /// <inheritdoc/>
    public CatalogDefinitionValidator(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this.RuleFor(c => c.Endpoint)
            .NotNull()
            .When(c => c.EndpointUri == null);
        this.RuleFor(c => c.EndpointUri)
            .NotNull()
            .When(c => c.Endpoint == null);
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

}