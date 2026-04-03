# Serverless Workflow .NET SDK

The official .NET SDK for the [Serverless Workflow DSL](https://github.com/serverlessworkflow/specification/blob/main/dsl.md).

The SDK is composed of three Nuget packages:

- [Core](#), which contains the models of the Serverless Workflow DSL
- [Builders](#), which contains service used to build workflow definitions programmatically
- [IO](#), which contains the services used to read and write workflow definitions

## Installation

Core:

```bash
dotnet add package ServerlessWorkflow.Sdk
```

Builders:

```
dotnet add package ServerlessWorkflow.Sdk.Builders
```

IO:

```
dotnet add package ServerlessWorkflow.Sdk.IO
```

## Example usage

Building a workflow definition programmatically:

```c#
var definition = new WorkflowDefinitionBuilder()
    .WithName("fake-workflow")
    .WithVersion("0.1.0:fake")
    .Do("todo-1", task => task
        .Call("http")
        .With("method", "get")
        .With("uri", "https://fake-api.com"))
    .Build();
```

Reading and writing a workflow definition:

```c#
using var inputStream = File.OpenRead("workflow.yaml");
var reader = WorkflowDefinitionReader.Create();
var workflow = await reader.ReadAsync(inputStream);

using var outputStream = File.Create("workflow.yaml");
var writer = WorkflowDefinitionWriter.Create();
await writer.WriteAsync(workflow, stream, WorkflowDefinitionFormat.Yaml);
```
