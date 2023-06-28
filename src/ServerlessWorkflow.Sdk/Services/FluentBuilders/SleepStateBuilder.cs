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
/// Represents the default implementation of the <see cref="IDelayStateBuilder"/> interface
/// </summary>
public class SleepStateBuilder
    : StateBuilder<SleepStateDefinition>, IDelayStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateDefinition"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public SleepStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual IDelayStateBuilder For(TimeSpan duration)
    {
        this.State.Duration = duration;
        return this;
    }

}
