![Publish Packages](https://github.com/serverlessworkflow/sdk-net/workflows/Publish%20Packages/badge.svg) [![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/serverlessworkflow/sdk-net)


# Serverless Workflow Specification - .NET SDK

Provides .NET 7.0 API/SPI and Model Validation for the [Serverless Workflow Specification](https://github.com/serverlessworkflow/specification)

With the SDK, you can:

- [x] Read and write workflow JSON and YAML definitions
- [x] Programmatically build workflow definitions
- [x] Validate workflow definitions (both schema and DSL integrity validation)

### Status

| Latest Releases | Conformance to spec version |
| :---: | :---: |
| [0.8.0.10](https://github.com/serverlessworkflow/sdk-net/releases/) | [0.8](https://github.com/serverlessworkflow/specification/tree/0.8.x) |

### Getting Started

```bash
dotnet nuget add package ServerlessWorkflow.Sdk
```

```csharp
services.AddServerlessWorkflow();
```

### Usage

#### Build workflows programatically

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
               action
                    .Consume(e =>
                        e.WithName("fakeEvent")
                            .WithSource(new Uri("https://fakesource.com"))
                            .WithType("fakeType"))
                    .ThenProduce(e =>
                        e.WithName("otherEvent")
                            .WithSource(new Uri("https://fakesource.com"))
                            .WithType("fakeType"));
          }))
  .End()
  .Build();
```

#### Read workflows

```csharp
var reader = WorkflowReader.Create();
using(Stream stream = File.OpenRead("myWorkflow.json"))
{
  var definition = reader.Read(stream, WorkflowDefinitionFormat.Json);
}
```

#### Write workflows

```csharp
  var writer = WorkflowWriter.Create();
  using(Stream stream = new MemoryStream())
  {
      writer.Write(workflow, stream);
      stream.Flush();
      stream.Position = 0;
      using(StreamReader reader = new StreamReader(stream))
      {
          var yaml = reader.ReadToEnd();
          Console.WriteLine(yaml);
          Console.ReadLine();
      }
  }
```

#### Validate workflows

```csharp
var validator = serviceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
var validationResult = validator.Validate(myWorkflow);
```

#### Extend Workflows

The SDK allows extending the Serverless Workflow in two ways, possibly combined: via metadata and via extensions.

#### Metadata

Workflow components that support metadata, such as `WorkflowDefinition` or `StateDefinition`, expose a `metadata` property, 
which is a dynamic name/value mapping of properties used to enrich the serverless workflow model with information beyond its core definitions.

It has the advantage of being an easy, cross-compatible way of declaring additional data, but lacks well-defined, well-documented schema of the data, thus loosing the ability to validate it
without custom implementation.

```csharp
var workflow = new WorkflowBuilder()
    ...
    .WithMetadata(new Dictionary<string, object>() { { "metadataPropertyName", metadataPropertyValue } })
    ...
    .Build();
```

#### Extension

Users have the ability to define extensions, providing the ability to extend, override or replace parts of the Serverless Workflow schema.

To do so, you must first create a file containing the JsonSchema of your extension, then reference it in your workflow definition.

*Schema of a sample extension that adds a new `greet` `functionType`:*
<table>
<tr>
    <th>JSON</th>
    <th>YAML</th>
</tr>
<tr>
<td valign="top">

```json
{
  "$defs": {
    "functions": {
      "definitions": {
        "function": {
          "type": "object",
          "properties": {
            "type": {
              "type": "string",
              "description": "Defines the function type. Is either `rest`, `asyncapi, `rpc`, `graphql`, `odata`, `expression` or `greet`. Default is `rest`",
              "enum": [
                "rest",
                "asyncapi",
                "rpc",
                "graphql",
                "odata",
                "expression",
                "custom",
                "greet"
              ],
              "default": "rest"
            }
          }
        }
      }
    }
  }
}
```

</td>

<td valign="top">

```yaml
'$defs':
  functions:
    definitions:
      function:
        type: object
        properties:
          type:
            type: string
            description: Defines the function type. Is either `rest`, `asyncapi, `rpc`,
              `graphql`, `odata`, `expression` or `greet`. Default is `rest`
            enum:
            - rest
            - asyncapi
            - rpc
            - graphql
            - odata
            - expression
            - custom
            - greet
            default: rest
```

</td>
</tr>
</table>

The above example refers to `/$defs/functions`, because upon validation the SDK bundles all the Serverless Workflow Specification's schemas into the `$defs` property of a single schema.

*In this case, `functions` is the extensionless name of the schema file we want to override (https://serverlessworkflow.io/schemas/latest/functions.json).

A [Json Merge Patch](https://datatracker.ietf.org/doc/html/rfc7386) is performed sequentially on the bundled schema with the defined extensions, in declaring order.

*In this case, the above schema will patch the object defined at `/functions/definitions/function` in file https://serverlessworkflow.io/schemas/latest/functions.json*

*Extending a workflow:*
```csharp
var workflow = new WorkflowBuilder()
    ...
    .UseExtension("extensionId", new Uri("file://.../extensions/greet-function-type.json"))
    ...
    .Build();
```