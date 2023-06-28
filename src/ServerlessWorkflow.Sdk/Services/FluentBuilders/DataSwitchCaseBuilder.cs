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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IDataSwitchCaseBuilder"/> interface
/// </summary>
public class DataSwitchCaseBuilder
    : SwitchCaseBuilder<IDataSwitchCaseBuilder, DataCaseDefinition>, IDataSwitchCaseBuilder
{

    /// <summary>
    /// Initializes a new <see cref="DataSwitchCaseBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="DataSwitchCaseBuilder"/> belongs to</param>
    public DataSwitchCaseBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {
        
    }

    /// <inheritdoc/>
    public virtual IDataSwitchCaseBuilder When(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) throw new ArgumentNullException(nameof(expression));
        this.Case.Condition = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual new DataCaseDefinition Build()
    {
        var outcome = base.Build();
        switch (outcome)
        {
            case EndDefinition end:
                this.Case.End = end;
                break;
            case TransitionDefinition transition:
                this.Case.Transition = transition;
                break;
            default:
                throw new NotSupportedException($"The specified outcome type '{outcome.GetType().Name}' is not supported");
        }
        return this.Case;
    }

}
