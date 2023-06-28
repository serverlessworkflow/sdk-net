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
/// Represents the default implementation of the <see cref="IStateOutcomeBuilder"/> interface
/// </summary>
public class StateOutcomeBuilder
    : IStateOutcomeBuilder
{

    /// <summary>
    /// Initializes a new <see cref="StateOutcomeBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateOutcomeBuilder"/> belongs to</param>
    public StateOutcomeBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="IStateOutcomeBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the <see cref="StateOutcomeDefinition"/> to configure
    /// </summary>
    protected StateOutcomeDefinition Outcome { get; set; } = null!;

    /// <inheritdoc/>
    public virtual void TransitionTo(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        if(stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        //TODO: configure transition
        var state = this.Pipeline.AddState(stateSetup);
        this.Outcome = new TransitionDefinition() { NextState = state.Name };
    }

    /// <inheritdoc/>
    public virtual void TransitionTo(string stateName)
    {
        if (string.IsNullOrWhiteSpace(stateName)) throw new ArgumentNullException(nameof(stateName));
        //TODO: configure transition
        this.Outcome = new TransitionDefinition() { NextState = stateName };
    }

    /// <inheritdoc/>
    public virtual void End()
    {
        //TODO: configure end
        this.Outcome = new EndDefinition();
    }

    /// <inheritdoc/>
    public virtual StateOutcomeDefinition Build()
    {
        return this.Outcome;
    }

}
