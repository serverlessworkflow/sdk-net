Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddFileLogging(args.GetLogFileOption());
builder.Services.AddServerlessWorkflowRuntime(builder.Configuration);
builder.Services.AddSingleton<RunWorkflowCommand>();

using var host = builder.Build();
await host.StartAsync();

var app = new CommandApp(new TypeRegistrar(host.Services));
app.Configure(config =>
{
    config.SetApplicationName("Serverless Workflow CLI");
    config.AddCommand<RunWorkflowCommand>("run")
        .WithDescription("Runs a workflow definition");
});

var exitCode = app.Run(args);
await host.StopAsync();
return exitCode;
