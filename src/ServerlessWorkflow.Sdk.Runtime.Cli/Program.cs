var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddServerlessWorkflowRuntime(builder.Configuration);
builder.Services.AddSingleton<RunWorkflowCommand>();

using var host = builder.Build();
await host.StartAsync();

var app = new CommandApp(new TypeRegistrar(host.Services));
app.Configure(config =>
{
    config.AddCommand<RunWorkflowCommand>("run");
});

var exitCode = app.Run(args);
await host.StopAsync();
return exitCode;