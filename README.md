# Serverless Workflow Specification - .NET SDK

Provides .NET 5.0 API/SPI and Model Validation for the [Serverless Workflow Specification](https://github.com/serverlessworkflow/specification)

With the SDK, you can:

- Parse workflow JSON and YAML definitions
- Programmatically build workflow definitions
- Validate workflow definitions (both schema and workflow integrity validation)

### Status

| Latest Releases | Conformance to spec version |
| :---: | :---: |
| [0.6.0](https://github.com/serverlessworkflow/sdk-net/releases/) | [v0.6](https://github.com/serverlessworkflow/specification/tree/0.6.x) |

### Getting Started

1. Add Serverless Workflow package source:
```bash
dotnet nuget add source "TODO"
```

2. Add nuget package:
```bash
dotnet nuget add package ServerlessWorkflow.Sdk
```

### How to use

#### Parsing workflows

```csharp
var parser = new WorkflowParser();
var yaml = File.ReadAllText("myworkflow.yaml");
var workflow = parser.Parse(yaml);
```

#### Building workflows programatically

```csharp
var workflow = WorkflowDefinition.Create("MyWorkflow", "MyWorkflow", "1.0")
  .StartsWith("inject", flow => 
      flow.Inject(new { username = "test", password = "123456" }))
  .Then("operation", flow =>
      flow.Execute("fakeApiFunctionCall", action =>
      {
          action.Invoke(function =>
              function.WithName("fakeFunction")
                  .SetOperationUri(new Uri("https://fake.com/swagger.json#fake")))
              .WithArgument("username", "${ .username }")
              .WithArgument("password", "${ .password }");
      })      
          .Execute("fakeEventTrigger", action =>
          {
              action.Consume(e =>
                  e.WithName("fakeEvent")
                      .WithSource(new Uri("https://fakesource.com"))
                      .WithType("fakeType"));
          }))
  .End()
  .Build();
```