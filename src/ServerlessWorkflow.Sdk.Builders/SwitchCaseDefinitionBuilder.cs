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
/// Represents the default implementation of the <see cref="ISwitchCaseDefinitionBuilder"/> interface
/// </summary>
public class SwitchCaseDefinitionBuilder
    : ISwitchCaseDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the runtime expression used to determine whether or not the case to build matches
    /// </summary>
    protected virtual string? WhenExpression { get; set; }

    /// <summary>
    /// Gets/sets the flow directive to execute when the case to build matches
    /// </summary>
    protected virtual string? ThenDirective { get; set; }

    /// <inheritdoc/>
    public virtual ISwitchCaseDefinitionBuilder When(string expression)
    {
        this.WhenExpression = expression;
        return this;
    }
    /// <inheritdoc/>
    public virtual ISwitchCaseDefinitionBuilder Then(string directive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directive);
        this.ThenDirective = directive;
        return this;
    }

    /// <inheritdoc/>
    public virtual SwitchCaseDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.ThenDirective)) throw new NullReferenceException("The flow directive to execute when the switch case matches must be set");
        return new()
        {
            When = this.WhenExpression,
            Then = this.ThenDirective
        };
    }

}