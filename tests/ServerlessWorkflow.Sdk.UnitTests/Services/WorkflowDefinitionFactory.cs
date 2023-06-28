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

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

public static class WorkflowDefinitionFactory
{

    public static WorkflowDefinition Create()
    {
        return new WorkflowBuilder()
            .WithId("fake")
            .WithName("Fake Workflow")
            .WithDescription("Fake Workflow Description")
            .UseSpecVersion(ServerlessWorkflowSpecVersion.Latest)
            .WithVersion("1.0.0")
            .WithAnnotation("fake-annotation: Fake value")
            .WithDataInputSchema(new Uri("https://tests.serverlessworkflow.io"))
            .WithMetadata(new Dictionary<string, object>() { { "fakeMetadataKey", "fakeMetadataValue" } })
            .WithExecutionTimeout(time => time.Run("fake-workflow:1.0.1").After(TimeSpan.FromSeconds(30)))
            .AddBasicAuthentication("fake-auth-basic", basic => basic.WithUserName("fake@email.com").WithPassword("0123456789"))
            .AddBearerAuthentication("fake-auth-bearer", bearer => bearer.WithToken("fake-token"))
            .AddOAuth2Authentication("fake-auth-oauth2", oauth2 => oauth2.UseGrantType(OAuth2GrantType.ClientCredentials).WithClientId("fake-client").WithPassword("fake-password"))
            .AddConstant("fake-constant", "fakeValue")
            .AddEvent(e => e.WithName("fake-event-consumed").WithSource(new Uri("https://tests.serverlessworkflow.io")).WithType("tests.serverlessworkflow.io").CorrelateUsing("subject").IsConsumed())
            .AddEvent(e => e.WithName("fake-event-produced").WithSource(new Uri("https://tests.serverlessworkflow.io")).WithType("tests.serverlessworkflow.io").CorrelateUsing("subject").IsProduced())
            .AddFunction(f => f.WithName("fake-function-asyncapi").UseAuthentication("fake-auth-basic").OfType(FunctionType.AsyncApi).ForOperation(new Uri("https://tests.serverlessworkflow.io#fakeOperationId")))
            .AddFunction(f => f.WithName("fake-function-expression").UseAuthentication("fake-auth-bearer").OfType(FunctionType.Expression).ForOperation("${ . }"))
            .AddFunction(f => f.WithName("fake-function-graphql").UseAuthentication("fake-auth-oauth2").OfType(FunctionType.GraphQL).ForOperation(new Uri("https://tests.serverlessworkflow.io#fakeOperationId")))
            .AddFunction(f => f.WithName("fake-function-odata").UseAuthentication("fake-auth-basic").OfType(FunctionType.OData).ForOperation(new Uri("https://tests.serverlessworkflow.io#fakeOperationId")))
            .AddFunction(f => f.WithName("fake-function-rest").UseAuthentication("fake-auth-bearer").OfType(FunctionType.Rest).ForOperation(new Uri("https://tests.serverlessworkflow.io#fakeOperationId")))
            .AddFunction(f => f.WithName("fake-function-rpc").UseAuthentication("fake-auth-oauth2").OfType(FunctionType.Rpc).ForOperation(new Uri("https://tests.serverlessworkflow.io#fakeOperationId")))
            .AddRetryStrategy(retry => retry.WithName("fakeRetry").WithDelayOf(TimeSpan.FromSeconds(2)).WithMaxDelay(TimeSpan.FromSeconds(10)).MaxAttempts(3))
            .AddSecret("fake-secret")
            .UseJq()
            .StartsWith("fake-inject", state => state.Inject(new { foo = "bar" }))
            .Then("fake-operation", state => state.Execute("fake-function-asyncapi", action => action.Invoke("fake-function-asyncapi").Invoke("fake-function-rest").WithArguments(new Dictionary<string, object>() { { "fake-argument-1", "fake-argument-1-value" }, { "fake-argument-2", "fake-argument-2-value" } })))
            .End()
            .Build();
    }

}
