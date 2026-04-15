# ServerlessWorkflow.Sdk.Runtime.Cli (`swf`)

A command-line runner for [Serverless Workflow DSL](https://github.com/serverlessworkflow/specification/blob/main/dsl.md) definitions, built on top of [ServerlessWorkflow.Sdk.Runtime](../ServerlessWorkflow.Sdk.Runtime).

The tool is published as a self-contained single-file executable named `swf`. It reads a workflow definition (JSON or YAML), executes it against the local runtime, and renders task progress, status, and duration in the terminal using Spectre.Console.

## Commands

### `run`

Executes a workflow definition.

```bash
swf run -f <workflow-file> [-i <input-file>] [-o <output-file>] [-l <log-file>]
```

| Option | Description |
|---|---|
| `-f, --file` | **Required.** Path to the workflow definition (`.json`, `.yaml`, `.yml`). |
| `-i, --input` | Path to an input data file (`.json`, `.yaml`, `.yml`). |
| `-o, --output` | Path to a file to write the workflow output to. |
| `-l, --log` | Path to a file to write runtime logs to (instead of the console). |

### Examples

Run a workflow with no input:

```bash
swf run -f workflow.yaml
```

Run a workflow with input, capture its output, and log to a file:

```bash
swf run -f workflow.yaml -i input.json -o result.json -l runtime.log
```

## Output

The CLI displays:

- Each task as it is pending, running, completed, faulted, skipped, cancelled, or suspended.
- Per-task duration.
- The final workflow output (or the file it was written to).

## Related packages

- [ServerlessWorkflow.Sdk](../ServerlessWorkflow.Sdk) — core DSL models
- [ServerlessWorkflow.Sdk.Builders](../ServerlessWorkflow.Sdk.Builders) — fluent builders
- [ServerlessWorkflow.Sdk.Runtime](../ServerlessWorkflow.Sdk.Runtime) — workflow runtime
- [Project root](../../README.md)
