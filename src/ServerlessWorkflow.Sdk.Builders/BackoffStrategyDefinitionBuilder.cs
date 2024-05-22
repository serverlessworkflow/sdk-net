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
/// Represents the default implementation of the <see cref="IBackoffStrategyDefinitionBuilder"/> interface
/// </summary>
public class BackoffStrategyDefinitionBuilder
    : IBackoffStrategyDefinitionBuilder
{

    /// <summary>
    /// Gets the underlying service used to build the <see cref="BackoffDefinition"/> to use
    /// </summary>
    protected IBackoffDefinitionBuilder? Backoff { get; set; }

    /// <inheritdoc/>
    public virtual IConstantBackoffDefinitionBuilder Constant()
    {
        var builder = new ConstantBackoffDefinitionBuilder();
        this.Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual IExponentialBackoffDefinitionBuilder Exponential()
    {
        var builder = new ExponentialBackoffDefinitionBuilder();
        this.Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual ILinearBackoffDefinitionBuilder Linear(Duration? increment = null)
    {
        var builder = new LinearBackoffDefinitionBuilder();
        this.Backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public virtual BackoffStrategyDefinition Build()
    {
        if (this.Backoff == null) throw new NullReferenceException("The backoff strategy must be set");
        return new()
        {
            Constant = this.Backoff is ConstantBackoffDefinition constant ? constant : null,
            Exponential = this.Backoff is ExponentialBackoffDefinition exponential ? exponential : null,
            Linear = this.Backoff is LinearBackoffDefinition linear ? linear : null,
        };
    }

}