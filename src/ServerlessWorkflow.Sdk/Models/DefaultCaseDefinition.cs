namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents an object used to define the transition of the workflow if there is no matching cases or event timeout is reached
/// </summary>
[DataContract]
public class DefaultCaseDefinition
    : SwitchCaseDefinition
{



}