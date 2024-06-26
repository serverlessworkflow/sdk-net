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
/// Represents the default implementation of the <see cref="IRaiseTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="errorDefinition">The error to raise</param>
public class RaiseTaskDefinitionBuilder(ErrorDefinition? errorDefinition = null)
    : TaskDefinitionBuilder<IRaiseTaskDefinitionBuilder, RaiseTaskDefinition>, IRaiseTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the error to raise
    /// </summary>
    protected ErrorDefinition? ErrorDefinition { get; set; } = errorDefinition;

    /// <inheritdoc/>
    public virtual IRaiseTaskDefinitionBuilder Error(ErrorDefinition error)
    {
        ArgumentNullException.ThrowIfNull(error);
        this.ErrorDefinition = error;
        return this;
    }

    /// <inheritdoc/>
    public virtual IRaiseTaskDefinitionBuilder Error(Action<IErrorDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorDefinitionBuilder();
        setup(builder);
        return this.Error(builder.Build());
    }

    /// <inheritdoc/>
    public override RaiseTaskDefinition Build()
    {
        if (this.ErrorDefinition == null) throw new NullReferenceException("The error to raise must be set");
        return new() 
        { 
            Raise = new() 
            { 
                Error = this.ErrorDefinition 
            } 
        };
    }

}
