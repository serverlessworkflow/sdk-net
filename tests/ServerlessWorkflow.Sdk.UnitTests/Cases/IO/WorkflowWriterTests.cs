// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

using ServerlessWorkflow.Sdk.Services.IO;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.IO;

public class WorkflowWriterTests
{

    protected IWorkflowWriter Writer { get; } = WorkflowWriter.Create();

    protected IWorkflowReader Reader { get; } = WorkflowReader.Create();

    protected static WorkflowDefinition BuildWorkflow()
    {
        return WorkflowDefinition.Create("MyWorkflow", "MyWorkflow", "1.0")
            .WithExecutionTimeout(timeout =>
                timeout.After(new TimeSpan(30, 2, 0, 0)))
            .StartsWith("inject", flow =>
                flow.Inject(new { username = "test", password = "123456"/*, scopes = new string[] { "api", "test" }*/ }))
            .Then("operation", flow =>
                flow.Execute("fakeApiFunctionCall", action =>
                {
                    action.Invoke(function =>
                        function.WithName("fakeFunction")
                            .ForOperation(new Uri("https://fake.com/swagger.json#fake")))
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
    }

    [Fact(Skip = "YAML parsing issue for non-complex properties (ex: externalRefs)")]
    public async Task Write_Yaml_ShouldWork()
    {
        var workflow = BuildWorkflow();
        using Stream stream = new MemoryStream();
        this.Writer.Write(workflow, stream);
        stream.Flush();
        stream.Position = 0;
        using StreamReader reader = new(stream);
        string yaml = reader.ReadToEnd();
        stream.Position = 0;
        workflow = await this.Reader.ReadAsync(stream);
        Assert.NotNull(workflow);
    }

    [Fact]
    public async Task Write_Json_ShoudlWork()
    {
        var toSerialize = BuildWorkflow();
        using var stream = new MemoryStream();
        this.Writer.Write(toSerialize, stream, WorkflowDefinitionFormat.Json);
        stream.Flush();
        using StreamReader reader = new(stream);
        string json = reader.ReadToEnd();
        stream.Position = 0;
        var deserialized = await this.Reader.ReadAsync(stream);
        Assert.NotNull(deserialized);
    }

}
