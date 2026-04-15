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
public sealed class BackoffStrategyDefinitionBuilder
    : IBackoffStrategyDefinitionBuilder
{

    IBackoffDefinitionBuilder? backoff;

    /// <inheritdoc/>
    public IConstantBackoffDefinitionBuilder Constant()
    {
        var builder = new ConstantBackoffDefinitionBuilder();
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public IExponentialBackoffDefinitionBuilder Exponential()
    {
        var builder = new ExponentialBackoffDefinitionBuilder();
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public ILinearBackoffDefinitionBuilder Linear(Duration? increment = null)
    {
        var builder = new LinearBackoffDefinitionBuilder(increment);
        backoff = builder;
        return builder;
    }

    /// <inheritdoc/>
    public BackoffStrategyDefinition Build()
    {
        if (backoff == null) throw new NullReferenceException("The backoff strategy must be set");
        var definition = backoff.Build();
        return new()
        {
            Constant = definition is ConstantBackoffDefinition constant ? constant : null,
            Exponential = definition is ExponentialBackoffDefinition exponential ? exponential : null,
            Linear = definition is LinearBackoffDefinition linear ? linear : null,
        };
    }

}
