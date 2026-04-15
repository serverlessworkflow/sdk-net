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

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class GenericTaskDefinitionBuilderTests
{

    [Fact]
    public void Call_Should_Return_CallTaskDefinition()
    {
        //arrange
        var functionName = "myFunc";
        var argName = "arg";
        var argValue = "val";
        var builder = new GenericTaskDefinitionBuilder();
        builder.Call(functionName).With(argName, JsonValue.Create(argValue));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<CallTaskDefinition>();
        ((CallTaskDefinition)task).Call.Should().Be(functionName);
    }

    [Fact]
    public void Set_With_Name_Value_Should_Return_SetTaskDefinition()
    {
        //arrange
        var key = "greeting";
        var value = "hello";
        var builder = new GenericTaskDefinitionBuilder();
        builder.Set(key, value);

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<SetTaskDefinition>();
    }

    [Fact]
    public void Set_With_JsonObject_Should_Return_SetTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var key = "k";
        var value = "v";
        builder.Set(new JsonObject { [key] = value });

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<SetTaskDefinition>();
    }

    [Fact]
    public void Wait_Should_Return_WaitTaskDefinition()
    {
        //arrange
        var duration = Duration.FromSeconds(5);
        var builder = new GenericTaskDefinitionBuilder();
        builder.Wait(duration);

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<WaitTaskDefinition>();
    }

    [Fact]
    public void Emit_With_EventDefinition_Should_Return_EmitTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var typeKey = "type";
        var typeValue = "com.test";
        var e = new EventDefinition { With = new JsonObject { [typeKey] = typeValue } };
        builder.Emit(e);

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<EmitTaskDefinition>();
    }

    [Fact]
    public void Emit_With_Setup_Should_Return_EmitTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var typeKey = "type";
        var typeValue = "com.test";
        builder.Emit(e => e.With(typeKey, JsonValue.Create(typeValue)));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<EmitTaskDefinition>();
    }

    [Fact]
    public void Raise_With_ErrorDefinition_Should_Return_RaiseTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var errorType = "https://err.com/t";
        var errorTitle = "T";
        var errorStatus = "500";
        var error = new ErrorDefinition { Type = errorType, Title = errorTitle, Status = errorStatus };
        builder.Raise(error);

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<RaiseTaskDefinition>();
    }

    [Fact]
    public void Raise_With_Setup_Should_Return_RaiseTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var errorType = "https://err.com/t";
        var errorTitle = "T";
        var errorStatus = "500";
        builder.Raise(e => e.WithType(errorType).WithTitle(errorTitle).WithStatus(errorStatus));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<RaiseTaskDefinition>();
    }

    [Fact]
    public void For_Should_Return_ForTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var eachVar = "item";
        var inExpr = "${ .items }";
        var taskName = "process";
        var key = "k";
        var value = "v";
        builder.For().Each(eachVar).In(inExpr).Do(tasks => tasks.Do(taskName, t => t.Set(key, value)));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<ForTaskDefinition>();
    }

    [Fact]
    public void Fork_Should_Return_ForkTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var b1Name = "b1";
        var b1Key = "a";
        var b1Value = "1";
        var b2Name = "b2";
        var b2Key = "b";
        var b2Value = "2";
        builder.Fork().Branch(tasks =>
        {
            tasks.Do(b1Name, t => t.Set(b1Key, b1Value));
            tasks.Do(b2Name, t => t.Set(b2Key, b2Value));
        });

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<ForkTaskDefinition>();
    }

    [Fact]
    public void Switch_Should_Return_SwitchTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var caseName = "default";
        builder.Switch().Case(caseName, c => c.Then(FlowDirective.End));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<SwitchTaskDefinition>();
    }

    [Fact]
    public void Try_Should_Return_TryTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var taskName = "risky";
        var key = "k";
        var value = "v";
        builder.Try()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Catch(c => { });

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<TryTaskDefinition>();
    }

    [Fact]
    public void Run_Should_Return_RunTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var command = "echo test";
        builder.Run().Shell().WithCommand(command);

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<RunTaskDefinition>();
    }

    [Fact]
    public void Listen_Should_Return_ListenTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var typeKey = "type";
        var typeValue = "com.test";
        builder.Listen().To(l => l.One().With(typeKey, JsonValue.Create(typeValue)));

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<ListenTaskDefinition>();
    }

    [Fact]
    public void Do_Should_Return_DoTaskDefinition()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();
        var s1Name = "s1";
        var s1Key = "a";
        var s1Value = "1";
        var s2Name = "s2";
        var s2Key = "b";
        var s2Value = "2";
        builder.Do(tasks =>
        {
            tasks.Do(s1Name, t => t.Set(s1Key, s1Value));
            tasks.Do(s2Name, t => t.Set(s2Key, s2Value));
        });

        //act
        var task = builder.Build();

        //assert
        task.Should().BeOfType<DoTaskDefinition>();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Task_Configured()
    {
        //arrange
        var builder = new GenericTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
