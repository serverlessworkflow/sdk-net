namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowScheduleDefinitionFactory
{
    internal static WorkflowScheduleDefinition CreateWithCron() => new()
    {
        Cron = "0 0 * * *"
    };

    internal static WorkflowScheduleDefinition CreateWithEvery() => new()
    {
        Every = Duration.FromMinutes(5)
    };

    internal static WorkflowScheduleDefinition CreateWithAfter() => new()
    {
        After = Duration.FromSeconds(30)
    };

    internal static WorkflowScheduleDefinition CreateWithEvent() => new()
    {
        On = EventConsumptionStrategyDefinitionFactory.CreateOne()
    };
}
