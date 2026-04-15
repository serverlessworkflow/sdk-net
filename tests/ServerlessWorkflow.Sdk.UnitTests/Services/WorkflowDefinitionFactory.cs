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

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowDefinitionFactory
{
    internal static WorkflowDefinition Create()
    {
        var tasks = new Map<string, TaskDefinition>
        {
            new MapEntry<string, TaskDefinition>("greet", TaskDefinitionFactory.CreateSetTask())
        };
        return new()
        {
            Document = WorkflowDefinitionMetadataFactory.Create(),
            Input = InputDataModelDefinitionFactory.Create(),
            Use = ComponentDefinitionCollectionFactory.Create(),
            Timeout = TimeoutDefinitionFactory.Create(),
            Output = OutputDataModelDefinitionFactory.Create(),
            Schedule = WorkflowScheduleDefinitionFactory.CreateWithCron(),
            Evaluate = RuntimeExpressionEvaluationConfigurationFactory.Create(),
            Do = tasks
        };
    }

    internal static WorkflowDefinition CreateMinimal()
    {
        var tasks = new Map<string, TaskDefinition>
        {
            new MapEntry<string, TaskDefinition>("greet", TaskDefinitionFactory.CreateSetTask())
        };
        return new()
        {
            Document = new WorkflowDefinitionMetadata
            {
                Dsl = "1.0.0",
                Name = "minimal-workflow",
                Version = "0.1.0"
            },
            Do = tasks
        };
    }
}
