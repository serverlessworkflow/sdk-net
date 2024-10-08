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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ISetTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="variables">A name/value mapping of the variables to set</param>
public class SetTaskDefinitionBuilder(IDictionary<string, object>? variables = null)
    : TaskDefinitionBuilder<ISetTaskDefinitionBuilder, SetTaskDefinition>, ISetTaskDefinitionBuilder
{

    /// <summary>
    /// Gets a name/value mapping of the variables to set
    /// </summary>
    protected EquatableDictionary<string, object> Variables { get; set; } = [..variables];

    /// <inheritdoc/>
    public virtual ISetTaskDefinitionBuilder Set(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Variables[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual ISetTaskDefinitionBuilder Set(IDictionary<string, object> variables)
    {
        ArgumentNullException.ThrowIfNull(variables);
        this.Variables = new(variables);
        return this;
    }

    /// <inheritdoc/>
    public override SetTaskDefinition Build() => this.Configure(new()
    {
        Set = this.Variables
    });

}
