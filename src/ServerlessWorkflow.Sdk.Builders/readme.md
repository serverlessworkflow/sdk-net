# ServerlessWorkflow.Sdk.Builders

Fluent builders for constructing [Serverless Workflow DSL](https://github.com/serverlessworkflow/specification/blob/main/dsl.md) definitions programmatically in .NET.

This package provides a strongly-typed, chainable API for authoring `WorkflowDefinition` instances without having to hand-write JSON or YAML.

## Installation

```bash
dotnet add package ServerlessWorkflow.Sdk.Builders
```

## Usage

```csharp
using ServerlessWorkflow.Sdk.Builders;

var definition = new WorkflowDefinitionBuilder()
    .UseDsl("1.0.0")
    .WithNamespace("samples")
    .WithName("fake-workflow")
    .WithVersion("0.1.0")
    .WithTitle("Fake Workflow")
    .WithSummary("A sample workflow that calls an HTTP endpoint.")
    .Do("fetch-data", task => task
        .Call("http")
        .With("method", "get")
        .With("uri", "https://fake-api.com"))
    .Build();
```

### Common builder methods

| Method | Purpose |
|---|---|
| `UseDsl(version)` | Sets the DSL version. |
| `WithNamespace(ns)` | RFC1123 DNS label namespace. |
| `WithName(name)` / `WithVersion(version)` | Identity of the workflow. |
| `WithTitle` / `WithSummary` / `WithTag` | Metadata. |
| `WithInput` / `WithOutput` | Input/output schemas. |
| `UseAuthentication` / `UseExtension` / `UseFunction` / `UseRetry` / `UseSecret` | Reusable components. |
| `Do(name, task => ...)` | Adds a task to the workflow. |
| `Build()` | Returns a `WorkflowDefinition`. |

Dedicated builders are available for every DSL construct — call, do, for, fork, listen, raise, run, set, switch, try, wait, retry policies, authentication schemes, and more.

## Related packages

- [ServerlessWorkflow.Sdk](../ServerlessWorkflow.Sdk) — core DSL models
- [ServerlessWorkflow.Sdk.Runtime](../ServerlessWorkflow.Sdk.Runtime) — workflow runtime
- [ServerlessWorkflow.Sdk.Runtime.Cli](../ServerlessWorkflow.Sdk.Runtime.Cli) — `swf` command-line runner
- [Project root](../../README.md)
