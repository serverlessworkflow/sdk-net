namespace ServerlessWorkflow.Sdk.Runtime;

public interface ITaskExecutionContext
{

    IWorkflowExecutionContext Workflow { get; }

    TaskDefinition Definition { get; }

    TaskInstance Instance { get; }

    JsonNode Input { get; }

    JsonObject ContextData { get; }

}