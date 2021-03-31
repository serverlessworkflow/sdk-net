/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Services
{

    public class WorkflowBuilderTests
    {

        protected IWorkflowBuilder WorkflowBuilder { get; } = new WorkflowBuilder();

        [Fact]
        public void Build()
        {
            //Arrange
            var id = "FakeWorkflow";
            var name = "FakeWorkflow";
            var description = "A fake workflow for test purposes";
            var version = "1.0";

            //Act
            var workflow = this.WorkflowBuilder
                .WithId(id)
                .WithName(name)
                .WithDescription(description)
                .WithVersion(version)
                .AddRetryStrategy(strat => 
                    strat.WithName("retry1")
                        .WithNoDelay()
                        .MaxAttempts(5))
                .StartsWith(flow => 
                    flow.Delay(TimeSpan.FromSeconds(3)))
                .Then(flow => 
                    flow.Inject(new JObject()))
                .Then(flow =>
                    flow.Execute(action => 
                        action.Invoke(function => 
                            function.WithName("login")
                                .SetOperationUri(new Uri("http://fakehost/api/doc/swagger.json#test")))
                            .WithArgument("username", "${ .username }")))
                .Then(state =>
                    state.ExecuteInParallel()
                        .WaitForAny()
                        .Branch(branch => 
                            branch.WithName("first")
                                .Concurrently()
                                .Execute(action =>
                                    action.Invoke(function =>
                                        function.WithName("login1")
                                            .SetOperationExpression("some workflow expression")))
                                .Execute(action =>
                                    action.Invoke(function =>
                                        function.WithName("login2")
                                            .SetOperationExpression("some workflow expression"))))
                        .Branch(branch =>
                            branch.WithName("second")
                                .Execute(action => 
                                    action.Consume(e => 
                                            e.WithName("Fake event")
                                                .IsConsumed()
                                                .WithSource(new Uri("https://fakesource"))
                                                .WithType("Fake type")
                                                .CorrelateUsing("correlationId", "${ .customerId }"))
                                        .ThenProduce("anotherevent")
                                        .WithContextAttribute("correlationId", null))))
                .Then(state =>
                    state.ForEach("${ .inputItems }", "item", "${ .outputItems }")
                        .Sequentially()
                        .Execute(action =>
                            action.Consume("somevent")
                                .ThenProduce("anotherevent")))
                .Then(flow =>
                    flow.Callback()
                        .Action(action => action.Invoke("login"))
                        .On("LoggedIn"))
                .Then(flow => 
                    flow.Events()
                        .Trigger(trigger =>
                            trigger.On("someevent")
                                .Execute(action => action.Invoke("test"))))
                .Then(flow =>
                    flow.Switch()
                        .Case(@case =>
                            @case.WithName("case1")
                                .WithExpression("${ .data.resultType == \"success\" }")
                                .End()))
                .End()
                .Build();

            //Assert
            Assert.NotNull(workflow);
            Assert.NotEmpty(workflow.Events);
            Assert.NotEmpty(workflow.Functions);
            Assert.NotEmpty(workflow.Retries);
            Assert.NotEmpty(workflow.States);
        }

    }

}
