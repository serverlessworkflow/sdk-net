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
