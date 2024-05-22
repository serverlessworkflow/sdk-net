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

using ServerlessWorkflow.Sdk.Builders;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowDefinitionFactory
{

    internal static WorkflowDefinition Create()
    {
        return new WorkflowDefinitionBuilder()
            .WithName("fake-name")
            .WithVersion("0.1.0")
            .WithTitle("Fake Title")
            .WithSummary("Fake MD summary")
            .WithTag("fakeTagName", "fakeTagValue")
            .UseAuthentication("fakeBasic", authentication => authentication
                .Basic()
                    .WithUsername("fake-user")
                    .WithPassword("fake-password"))
            .UseAuthentication("fakeBearer", authentication => authentication
                .Bearer()
                    .WithToken("fake-token"))
            .UseAuthentication("fakeOAuth2", authentication => authentication
                .OAuth2()
                    .WithAuthority(new("https://fake-authority.com"))
                    .WithGrantType(OAuth2GrantType.ClientCredentials)
                    .WithClient(client => client
                        .WithId("fake-client-id")
                        .WithSecret("fake-client-secret")))
            .UseFunction("fakeFunction1", function => function
                .Function("http")
                    .With("method", "post")
                    .With("uri", "https://test.com"))
            .UseFunction("fakeFunction2", function => function
                .Shell()
                    .WithCommand(@"echo ""Hello, World!"""))
            .UseExtension("fakeLoggingExtension", extension => extension
                .ExtendAll()
                .When("fake-expression")
                .Before(task => task
                    .Call("http")
                        .With("method", "post")
                        .With("uri", "https://fake.log.collector.com")
                        .With("body", new
                        {
                            message = @"${ ""Executing task '\($task.reference)'..."" }"
                        }))
                .After(task => task
                    .Call("http")
                        .With("method", "post")
                        .With("uri", "https://fake.log.collector.com")
                        .With("body", new
                        {
                            message = @"${ ""Executed task '\($task.reference)'..."" }"
                        })))
            .UseSecret("fake-secret")
            .Do("todo-1", task => task
                .Call("http")
                .With("method", "get")
                .With("uri", "https://unit-tests.serverlessworkflow.io"))
            .Do("todo-2", task => task
                .Emit(e => e
                    .With("type", "io.serverlessworkflow.unit-tests.fake.event.type.v1")))
            .Do("todo-3", task => task
                .For()
                    .Each("color")
                    .In(".colors")
                    .At("index")
                    .Do(subtask => subtask
                        .Set("processed", ".processed + [$color]")))
            .Do("todo-4", task => task
                .Listen()
                    .To(to => to
                        .Any()
                            .Event(e => e
                                .With("foo", "bar"))
                            .Event(e => e
                                .With(new Dictionary<string, object>() { { "foo", "bar" }, { "bar", "baz" } }))))
            .Do("todo-5", task => task
                .Raise(error => error
                    .WithType("fake-error-type")
                    .WithStatus("400")
                    .WithTitle("fake-error-title")))
            .Do("todo-6", task => task
                .Run()
                    .Container()
                        .WithImage("fake-image:latest")
                        .WithCommand("fake command --arg1 arg1")
                        .WithEnvironment("ASPNET_ENVIRONMENT", "Development"))
            .Do("todo-7", task => task
                .Run()
                    .Shell()
                        .WithCommand("fake command --arg1 arg1")
                        .WithArgument("--arg2 arg2")
                        .WithEnvironment("ASPNET_ENVIRONMENT", "Development"))
            .Do("todo-8", task => task
                .Run()
                    .Script()
                        .WithLanguage("js")
                        .WithCode(@"console.log(""Hello, World!"")"))
            .Do("todo-9", task => task
                .Run()
                    .Workflow()
                        .WithName("fake-workflow")
                        .WithVersion("1.0.0")
                        .WithInput(new { foo = "bar" }))
            .Do("todo-10", task => task
                .Set("foo", "bar")
                .Set("bar", new { baz = "foo" }))
            .Do("todo-11", task => task
                .Switch()
                    .Case("case-1", @case => @case
                        .When("fake-condition")
                        .Then(FlowDirective.Continue))
                    .Case("case-2", @case => @case
                        .When("another-fake-condition")
                        .Then(FlowDirective.Exit))
                    .Case("default", @case => @case
                        .Then(FlowDirective.End)))
            .Do("todo-12", task => task
                .Try()
                    .Do(subtask => subtask
                        .Set("foo", "bar"))
                .Catch(error => error
                    .Errors(filter => filter
                        .With("status", ". == 400"))
                    .As("error")
                    .When("fake-condition")
                    .ExceptWhen("another-fake-condition")
                    .Retry(retry => retry
                        .When("fake-condition")
                        .ExceptWhen("another-fake-condition")
                        .Limit(limits => limits
                            .Attempt()
                                .Count(10)))
                    .Do(subtask => subtask
                        .Set("foo", "bar"))))
            .Do("todo-13", task => task
                .Wait()
                    .For(Duration.FromMinutes(5)))
            .Do("todo-14", task => task
                .Execute()
                    .Sequentially(tasks => tasks
                        .Do("todo-14-1", task => task
                            .Call("http")
                                .With("method", "get")
                                .With("uri", "https://unit-tests.serverlessworkflow.io"))
                        .Do("todo-14-2", task => task
                            .Emit(e => e
                                .With("type", "io.serverlessworkflow.unit-tests.fake.event.type.v1")))
                        .Do("todo-14-3", task => task
                            .For()
                                .Each("color")
                                .In(".colors")
                                .At("index")
                            .Do(subtask => subtask
                                .Set("processed", ".processed + [$color]")))))
            .Build();
    }

}
