using System.Diagnostics;
using System.Threading.Channels;
using ServerlessWorkflow.Sdk.Events.Tasks;
using ServerlessWorkflow.Sdk.Events.Workflows;
using Spectre.Console.Json;

namespace ServerlessWorkflow.Sdk.Runtime.Cli.Commands;

internal sealed class RunWorkflowCommand(IWorkflowRuntime workflowRuntime, ICloudEventBus cloudEventBus)
    : AsyncCommand<RunWorkflowCommand.Settings>
{

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var file = new FileInfo(settings.WorkflowFile);
        if (!file.Exists)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]✗ Workflow file not found:[/] [yellow]{settings.WorkflowFile}[/]");
            return -1;
        }
        WorkflowDefinition definition;
        try
        {
            definition = await LoadAsync<WorkflowDefinition>(file, cancellationToken).ConfigureAwait(false) ?? throw new NullReferenceException($"Failed to deserialize workflow definition from '{file.FullName}'");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]✗ Failed to load workflow:[/] {ex.Message}");
            return -1;
        }
        JsonObject? input = null;
        if (!string.IsNullOrWhiteSpace(settings.InputFile))
        {
            var inputFile = new FileInfo(settings.InputFile);
            if (!inputFile.Exists)
            {
                AnsiConsole.MarkupLineInterpolated($"[red]✗ Input file not found:[/] [yellow]{settings.InputFile}[/]");
                return -1;
            }
            try { input = await LoadAsync<JsonObject>(inputFile, cancellationToken).ConfigureAwait(false); }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLineInterpolated($"[red]✗ Failed to load input:[/] {ex.Message}");
                return -1;
            }
        }
        RenderBanner(definition, file, settings);
        var channel = Channel.CreateUnbounded<ICloudEvent>(new UnboundedChannelOptions { SingleReader = true });
        var observable = await cloudEventBus.SubscribeAsync(cancellationToken).ConfigureAwait(false);
        using var subscription = observable.Subscribe(
            ev => channel.Writer.TryWrite(ev),
            _ => channel.Writer.TryComplete(),
            () => channel.Writer.TryComplete());
        var tracker = new WorkflowRunTracker();
        var table = BuildTable();
        var stopwatch = new Stopwatch();
        var initialLayout = new Rows(RenderStatus(tracker, TimeSpan.Zero, 0), table);

        Task? runTask = null;
        try
        {
            await AnsiConsole.Live(initialLayout)
                .AutoClear(false)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Bottom)
                .StartAsync(async ctx =>
                {
                    UpdateTable(table, tracker);
                    ctx.UpdateTarget(new Rows(RenderStatus(tracker, TimeSpan.Zero, 0), table));
                    ctx.Refresh();

                    stopwatch.Start();
                    runTask = Task.Run(async () =>
                    {
                        var process = await workflowRuntime.RunAsync(definition, input, new(), cancellationToken).ConfigureAwait(false);
                        try { await process.WaitAsync(cancellationToken).ConfigureAwait(false); }
                        catch { /* faulted state captured via events */ }
                    }, cancellationToken);

                    var frame = 0;
                    while (true)
                    {
                        while (channel.Reader.TryRead(out var ev)) tracker.Apply(ev);
                        UpdateTable(table, tracker);
                        ctx.UpdateTarget(new Rows(RenderStatus(tracker, stopwatch.Elapsed, frame++), table));
                        ctx.Refresh();
                        if (runTask.IsCompleted) break;
                        await Task.Delay(80, cancellationToken).ConfigureAwait(false);
                    }
                    while (channel.Reader.TryRead(out var ev)) tracker.Apply(ev);
                    UpdateTable(table, tracker);
                    ctx.UpdateTarget(new Rows(RenderStatus(tracker, stopwatch.Elapsed, frame), table));
                    ctx.Refresh();
                }).ConfigureAwait(false);
        }
        finally
        {
            stopwatch.Stop();
            if (runTask != null) { try { await runTask.ConfigureAwait(false); } catch { } }
        }
        RenderResult(tracker, stopwatch.Elapsed, settings);
        if (tracker.Status == WorkflowStatus.Completed) return 0;
        return 1;
    }

    static string FormatDuration(TimeSpan d) => $"{d.TotalSeconds:0.000000}s";

    static string ToRelativePath(string path)
    {
        try
        {
            var relative = Path.GetRelativePath(Directory.GetCurrentDirectory(), path);
            return relative.Length < path.Length ? relative : path;
        }
        catch
        {
            return path;
        }
    }

    static async Task<T?> LoadAsync<T>(FileInfo file, CancellationToken cancellationToken)
        where T : class
    {
        var text = await File.ReadAllTextAsync(file.FullName, cancellationToken).ConfigureAwait(false);
        return file.Extension.ToLowerInvariant() switch
        {
            ".json" => JsonSerializer.Deserialize<T>(text, Sdk.Serialization.Json.JsonSerializationContext.Default.Options),
            ".yaml" or ".yml" => YamlSerializer.Deserialize<T>(text, Sdk.Serialization.Json.JsonSerializationContext.Default.Options),
            _ => throw new NotSupportedException($"Unsupported file extension '{file.Extension}'")
        };
    }

    static void RenderBanner(WorkflowDefinition definition, FileInfo file, Settings settings)
    {
        var name = definition.Document?.Name ?? file.Name;
        var version = definition.Document?.Version ?? "-";
        var ns = definition.Document?.Namespace ?? "-";
        var figlet = new FigletText("Serverless Workflow").Color(Color.DeepSkyBlue1).LeftJustified();
        var info = new Grid()
            .AddColumn(new GridColumn().NoWrap().PadRight(2))
            .AddColumn(new GridColumn().NoWrap().PadRight(2))
            .AddColumn()
            .AddRow("[deepskyblue1]❯[/]", "[grey]workflow[/]", $"[bold white]{Markup.Escape(name)}[/]")
            .AddRow("[deepskyblue1]❯[/]", "[grey]namespace[/]", $"[white]{Markup.Escape(ns)}[/]")
            .AddRow("[deepskyblue1]❯[/]", "[grey]version[/]", $"[white]{Markup.Escape(version)}[/]")
            .AddRow("[deepskyblue1]❯[/]", "[grey]source[/]", $"[white]{Markup.Escape(ToRelativePath(file.FullName))}[/]");
        if (!string.IsNullOrWhiteSpace(settings.InputFile)) info.AddRow("[deepskyblue1]❯[/]", "[grey]input[/]", $"[white]{Markup.Escape(ToRelativePath(settings.InputFile))}[/]");
        if (!string.IsNullOrWhiteSpace(settings.OutputFile)) info.AddRow("[deepskyblue1]❯[/]", "[grey]output[/]", $"[white]{Markup.Escape(ToRelativePath(settings.OutputFile))}[/]");
        if (!string.IsNullOrWhiteSpace(settings.LogFile)) info.AddRow("[deepskyblue1]❯[/]", "[grey]log[/]", $"[white]{Markup.Escape(ToRelativePath(settings.LogFile))}[/]");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(figlet);
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule { Style = new Style(Color.DeepSkyBlue1) });
        AnsiConsole.Write(new Padder(info).PadTop(1).PadBottom(1).PadLeft(1));
        AnsiConsole.Write(new Rule { Style = new Style(Color.Grey35) });
        AnsiConsole.WriteLine();
    }

    static Table BuildTable()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey35)
            .Expand();
        table.AddColumn(new TableColumn("[grey]  [/]").Width(3).NoWrap());
        table.AddColumn(new TableColumn("[grey]Task[/]"));
        table.AddColumn(new TableColumn("[grey]Status[/]").Width(14).NoWrap());
        table.AddColumn(new TableColumn("[grey]Duration[/]").RightAligned().Width(12).NoWrap());
        return table;
    }

    static void UpdateTable(Table table, WorkflowRunTracker tracker)
    {
        table.Rows.Clear();
        foreach (var task in tracker.Tasks.Values)
        {
            var (glyph, color, label) = task.Status switch
            {
                TaskRunStatus.Pending => ("•", "grey", "pending"),
                TaskRunStatus.Running => (Spinner.Known.Dots.Frames[0], "deepskyblue1", "running"),
                TaskRunStatus.Completed => ("✓", "green", "completed"),
                TaskRunStatus.Faulted => ("✗", "red", "faulted"),
                TaskRunStatus.Skipped => ("»", "yellow", "skipped"),
                TaskRunStatus.Cancelled => ("⊘", "orange3", "cancelled"),
                TaskRunStatus.Suspended => ("‖", "grey70", "suspended"),
                _ => ("?", "grey", "unknown")
            };
            var duration = task.Duration is { } d ? FormatDuration(d) : task.Status == TaskRunStatus.Running ? "[grey]…[/]" : "[grey]-[/]";
            var reference = Markup.Escape(task.Reference);
            table.AddRow(
                $"[{color}]{glyph}[/]",
                $"[white]{reference}[/]",
                $"[{color}]{label}[/]",
                duration);
        }
        if (tracker.Tasks.Count == 0) table.AddRow("[grey]·[/]", "[grey]waiting for first task…[/]", string.Empty, string.Empty);
    }

    static readonly string[] SpinnerFrames = ["⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏"];

    static Markup RenderStatus(WorkflowRunTracker tracker, TimeSpan elapsed, int frame)
    {
        var (color, label, running) = tracker.Status switch
        {
            WorkflowStatus.Running => ("deepskyblue1", "running", true),
            WorkflowStatus.Completed => ("green", "completed", false),
            WorkflowStatus.Faulted => ("red", "faulted", false),
            WorkflowStatus.Cancelled => ("orange3", "cancelled", false),
            WorkflowStatus.Suspended => ("grey70", "suspended", false),
            _ => ("deepskyblue1", "pending", true)
        };
        var glyph = running ? SpinnerFrames[frame % SpinnerFrames.Length] : "●";
        return new Markup($"  [{color}]{glyph}[/] [bold {color}]{label}[/]  [grey]·[/]  [white]{elapsed:mm\\:ss\\.fff}[/]  [grey]·[/]  [white]{tracker.CompletedCount}[/][grey]/[/][white]{tracker.Tasks.Count}[/] [grey]tasks[/]");
    }

    static void RenderResult(WorkflowRunTracker tracker, TimeSpan elapsed, Settings settings)
    {
        AnsiConsole.WriteLine();
        var (color, glyph, label) = tracker.Status switch
        {
            WorkflowStatus.Completed => (Color.Green, "✓", "COMPLETED"),
            WorkflowStatus.Faulted => (Color.Red, "✗", "FAULTED"),
            WorkflowStatus.Cancelled => (Color.Orange3, "⊘", "CANCELLED"),
            WorkflowStatus.Suspended => (Color.Grey70, "‖", "SUSPENDED"),
            _ => (Color.Grey, "?", "UNKNOWN")
        };

        var summary = new Grid()
            .AddColumn(new GridColumn().NoWrap().PadRight(2))
            .AddColumn()
            .AddRow("[grey]status[/]", $"[bold {color.ToMarkup()}]{glyph} {label}[/]")
            .AddRow("[grey]duration[/]", $"[white]{FormatDuration(elapsed)}[/]")
            .AddRow("[grey]tasks[/]", $"[green]{tracker.CompletedCount} completed[/]  [yellow]{tracker.SkippedCount} skipped[/]  [red]{tracker.FaultedCount} faulted[/]  [white]{tracker.Tasks.Count} total[/]");

        AnsiConsole.Write(new Panel(summary)
        {
            Header = new PanelHeader($" [bold {color.ToMarkup()}]workflow {label.ToLowerInvariant()}[/] "),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(color),
            Padding = new Padding(1, 0, 1, 0),
            Expand = true
        });

        if (tracker.Error != null)
        {
            var errGrid = new Grid().AddColumn(new GridColumn().NoWrap().PadRight(2)).AddColumn();
            if (tracker.Error.Type != null) errGrid.AddRow("[grey]type[/]", $"[red]{Markup.Escape(tracker.Error.Type.ToString()!)}[/]");
            if (!string.IsNullOrWhiteSpace(tracker.Error.Title)) errGrid.AddRow("[grey]title[/]", $"[red]{Markup.Escape(tracker.Error.Title!)}[/]");
            if (tracker.Error.Status != 0) errGrid.AddRow("[grey]status[/]", $"[red]{tracker.Error.Status}[/]");
            if (!string.IsNullOrWhiteSpace(tracker.Error.Detail)) errGrid.AddRow("[grey]detail[/]", $"[red]{Markup.Escape(tracker.Error.Detail!)}[/]");
            AnsiConsole.Write(new Panel(errGrid)
            {
                Header = new PanelHeader(" [bold red]error[/] "),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Red),
                Padding = new Padding(1, 0, 1, 0),
                Expand = true
            });
        }

        if (tracker.Output != null)
        {
            var json = tracker.Output.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
            var outputPanel = new Panel(new JsonText(json))
            {
                Header = new PanelHeader(" [bold deepskyblue1]output[/] "),
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.DeepSkyBlue1),
                Padding = new Padding(1, 0, 1, 0),
                Expand = true
            };
            AnsiConsole.Write(outputPanel);

            if (!string.IsNullOrWhiteSpace(settings.OutputFile))
            {
                try
                {
                    var dir = Path.GetDirectoryName(Path.GetFullPath(settings.OutputFile));
                    if (!string.IsNullOrWhiteSpace(dir)) Directory.CreateDirectory(dir);
                    File.WriteAllText(settings.OutputFile, json);
                    AnsiConsole.MarkupLineInterpolated($"[grey]→ output written to[/] [white]{settings.OutputFile}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLineInterpolated($"[red]✗ Failed to write output file:[/] {ex.Message}");
                }
            }
        }

        AnsiConsole.WriteLine();
    }

    internal sealed class Settings
        : CommandSettings
    {

        [CommandOption("-f|--file")]
        [Description("Path to the workflow definition file (.json, .yaml, .yml)")]
        public required string WorkflowFile { get; init; }

        [CommandOption("-i|--input")]
        [Description("Path to an input data file (.json, .yaml, .yml)")]
        public string? InputFile { get; init; }

        [CommandOption("-o|--output")]
        [Description("Write the workflow's output to the specified file")]
        public string? OutputFile { get; init; }

        [CommandOption("-l|--log")]
        [Description("Write runtime logs to the specified file (instead of polluting the console)")]
        public string? LogFile { get; init; }

    }

    enum TaskRunStatus { Pending, Running, Completed, Faulted, Skipped, Cancelled, Suspended }

    sealed class TaskRunInfo
    {
        public required string Reference { get; init; }
        public TaskRunStatus Status { get; set; } = TaskRunStatus.Pending;
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
        public TimeSpan? Duration => StartedAt.HasValue && EndedAt.HasValue ? EndedAt - StartedAt : null;
    }

    sealed class WorkflowRunTracker
    {

        public Dictionary<string, TaskRunInfo> Tasks { get; } = new();
        public string Status { get; private set; } = WorkflowStatus.Pending;
        public JsonNode? Output { get; private set; }
        public Error? Error { get; private set; }
        public int CompletedCount { get; private set; }
        public int FaultedCount { get; private set; }
        public int SkippedCount { get; private set; }

        public void Apply(ICloudEvent ev)
        {
            switch (ev.Data)
            {
                case WorkflowStartedEvent:
                    Status = WorkflowStatus.Running;
                    break;
                case WorkflowCompletedEvent completed:
                    Status = WorkflowStatus.Completed;
                    Output = ToJsonNode(completed.Output);
                    break;
                case WorkflowFaultedEvent faulted:
                    Status = WorkflowStatus.Faulted;
                    Error = faulted.Error;
                    break;
                case WorkflowCancelledEvent:
                    Status = WorkflowStatus.Cancelled;
                    break;
                case WorkflowSuspendedEvent:
                    Status = WorkflowStatus.Suspended;
                    break;
                case WorkflowResumedEvent:
                    Status = WorkflowStatus.Running;
                    break;
                case TaskCreatedEvent created:
                    GetOrAdd(created.Task);
                    break;
                case TaskStartedEvent started:
                    {
                        var task = GetOrAdd(started.Task);
                        task.Status = TaskRunStatus.Running;
                        task.StartedAt = started.StartedAt;
                        break;
                    }
                case TaskCompletedEvent taskCompleted:
                    {
                        var task = GetOrAdd(taskCompleted.Task);
                        if (task.Status != TaskRunStatus.Completed)
                        {
                            task.Status = TaskRunStatus.Completed;
                            task.EndedAt = taskCompleted.CompletedAt;
                            CompletedCount++;
                        }
                        break;
                    }
                case TaskFaultedEvent taskFaulted:
                    {
                        var task = GetOrAdd(taskFaulted.Task);
                        task.Status = TaskRunStatus.Faulted;
                        task.EndedAt = taskFaulted.FaultedAt;
                        FaultedCount++;
                        break;
                    }
                case TaskSkippedEvent skipped:
                    {
                        var task = GetOrAdd(skipped.Task);
                        task.Status = TaskRunStatus.Skipped;
                        task.EndedAt = skipped.SkippedAt;
                        SkippedCount++;
                        break;
                    }
                case TaskCancelledEvent cancelled:
                    {
                        var task = GetOrAdd(cancelled.Task);
                        task.Status = TaskRunStatus.Cancelled;
                        task.EndedAt = cancelled.CancelledAt;
                        break;
                    }
                case TaskSuspendedEvent suspended:
                    {
                        var task = GetOrAdd(suspended.Task);
                        task.Status = TaskRunStatus.Suspended;
                        break;
                    }
            }
        }

        TaskRunInfo GetOrAdd(Json.Pointer.JsonPointer reference)
        {
            var key = reference.ToString();
            if (!Tasks.TryGetValue(key, out var task))
            {
                task = new TaskRunInfo { Reference = key };
                Tasks[key] = task;
            }
            return task;
        }

        static JsonNode? ToJsonNode(object? value) => value switch
        {
            null => null,
            JsonNode node => node.DeepClone(),
            _ => JsonSerializer.SerializeToNode(value, Sdk.Serialization.Json.JsonSerializationContext.Default.Options)
        };

    }

}

file static class ColorExtensions
{

    public static string ToMarkup(this Color color) => color.ToString();

}
