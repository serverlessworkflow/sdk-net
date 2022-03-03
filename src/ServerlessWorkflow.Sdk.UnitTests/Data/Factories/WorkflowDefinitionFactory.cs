using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Schema.Generation;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System;

namespace ServerlessWorkflow.Sdk.UnitTests.Data.Factories
{

    public static class WorkflowDefinitionFactory
    {

        public static WorkflowDefinition Create()
        {
            var services = new ServiceCollection();
            services.AddServerlessWorkflow();
            var builder = services.BuildServiceProvider().GetRequiredService<IWorkflowBuilder>();
            return builder
                .WithId("fake-workflow")
                .WithVersion("0.1.0-fake")
                .WithName("Fake Workflow")
                .WithDataInputSchema(new JSchemaGenerator().Generate(typeof(TestInputData)))
                .AddFunction(function =>
                    function.WithName("fake-function")
                        .OfType(FunctionType.Rest)
                        .ForOperation("https://fake.com#fake"))
                .StartsWith("fake-state", state => 
                    state.Execute("fake-operation", operation =>
                        operation.Invoke("fake-function")))
                .End()
                .Build();
        }

    }

    class TestInputData
    {

        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;

        public string Comments { get; set; } = "None";

    }

}
