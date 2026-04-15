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

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowExecutionContextFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="expressionEvaluatorProvider">The service used to resolve <see cref="IRuntimeExpressionEvaluator"/>s based on the specified language</param>
public sealed class WorkflowExecutionContextFactory(IServiceProvider serviceProvider, IRuntimeExpressionEvaluatorProvider expressionEvaluatorProvider)
    : IWorkflowExecutionContextFactory
{

    /// <inheritdoc/>
    public IWorkflowExecutionContext Create(WorkflowDefinition definition, IWorkflowInstance state, WorkflowExecutionsOptions executionsOptions)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(executionsOptions);
        var language = definition.Evaluate?.Language ?? RuntimeExpressions.Languages.JQ;
        var expressionEvaluator = expressionEvaluatorProvider.GetEvaluator(language) ?? throw new NullReferenceException($"Failed to resolve an expression evaluator for the specified language '{language}'");
        return ActivatorUtilities.CreateInstance<WorkflowExecutionContext>(serviceProvider, definition, state, executionsOptions, expressionEvaluator);
    }

}
