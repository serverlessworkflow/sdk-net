// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
