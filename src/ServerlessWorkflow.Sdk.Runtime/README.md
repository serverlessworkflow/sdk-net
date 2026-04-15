# ServerlessWorkflow.Sdk.Runtime

A .NET runtime for executing [Serverless Workflow DSL](https://github.com/serverlessworkflow/specification/blob/main/dsl.md) definitions.

This package ships the services required to load, schedule, and execute workflow definitions, including task executors, expression evaluation (JQ and JavaScript via Jint), authentication, secret management, schema validation (JSON Schema, Avro, XML), and CloudEvents integration.

## Installation

```bash
dotnet add package ServerlessWorkflow.Sdk.Runtime
```

## Usage

Register the runtime with `Microsoft.Extensions.DependencyInjection`:

```csharp
using ServerlessWorkflow.Sdk.Runtime;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddServerlessWorkflowRuntime(builder.Configuration);
```

Then resolve and use `IWorkflowRuntime` to run a workflow:

```csharp
var runtime = host.Services.GetRequiredService<IWorkflowRuntime>();

var process = await runtime.RunAsync(definition, input, executionOptions: null, cancellationToken);
await process.WaitAsync(cancellationToken);
```

You can also run a workflow by reference:

```csharp
var process = await runtime.RunAsync(
    @namespace: "samples",
    name: "fake-workflow",
    version: "0.1.0",
    input: input,
    executionOptions: null,
    cancellationToken);
```

## Key services

| Service | Role |
|---|---|
| `IWorkflowRuntime` | Entry point for running workflows. |
| `IWorkflowDefinitionStore` | Persists and retrieves workflow definitions. |
| `IWorkflowStateStore` | Persists workflow execution state. |
| `IWorkflowProcessFactory` | Creates `IWorkflowProcess` instances for execution. |
| `ITaskExecutor` / `TaskExecutorFactory` | Execute individual DSL tasks. |
| `RuntimeExpressionEvaluator` | Evaluates runtime expressions (JQ / JavaScript). |
| `AuthenticationHandler` / `OAuth2TokenManager` | Handles workflow authentication. |
| `SecretsManager` | Resolves secret references at runtime. |
| `ICloudEventBus` | Publishes and subscribes to CloudEvents. |

## Related packages

- [ServerlessWorkflow.Sdk](../ServerlessWorkflow.Sdk) — core DSL models
- [ServerlessWorkflow.Sdk.Builders](../ServerlessWorkflow.Sdk.Builders) — fluent builders
- [ServerlessWorkflow.Sdk.Runtime.Cli](../ServerlessWorkflow.Sdk.Runtime.Cli) — `swf` command-line runner
- [Project root](../../README.md)
