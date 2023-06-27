namespace ServerlessWorkflow.Sdk.UnitTests.Cases.FluentApi;

public class FluentBuilderTests
{

    [Fact]
    public void Build_GenericState_Should_Work()
    {
        //arrange
        var stateBuilder = new InjectStateBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var name = "fake-state-name";
        var metadata = new Dictionary<string, object>() { { "fake-extension-property", new { foo = new { bar = "baz" } } } };
        var compensationState = "fake-compensation-state";
        var filterInputExpression = "${ . }";
        var filterOutputExpression = "${ . }";
        var error = "fake-error";
        var errorCode = "fake-error-code";

        //act
        var state = (InjectStateDefinition)stateBuilder
            .WithName(name)
            .WithMetadata(metadata)
            .CompensateWith(compensationState)
            .FilterInput(filterInputExpression)
            .FilterOutput(filterOutputExpression)
            .HandleError(ex => ex.Catch(error, errorCode).Then(flow => flow.End()))
            .Build();

        //assert
        state.Should().NotBeNull();
        state.Name.Should().Be(name);
        state.Metadata.Should().BeEquivalentTo(metadata);
        state.CompensatedBy.Should().Be(compensationState);
        state.DataFilter.Should().NotBeNull();
        state.DataFilter!.Input.Should().Be(filterInputExpression);
        state.DataFilter!.Output.Should().Be(filterOutputExpression);
        state.Errors.Should().ContainSingle();
        state.Errors![0].Error.Should().Be(error);
        state.Errors![0].Code.Should().Be(errorCode);
    }

    [Fact]
    public void Build_CallbackState_Should_Work()
    {
        //arrange
        var eventName = "fake-event";
        var action1Name = "fake-action-1";
        var functionName = "fake-function";
        var stateBuilder = new CallbackStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (CallbackStateDefinition)stateBuilder
            .On(eventName)
            .Execute(action1Name, action => action.Invoke(function => function.OfType(FunctionType.Expression).WithName(functionName)))
            .Build();

        //assert
        state.Should().NotBeNull();
        state.EventRef.Should().Be(eventName);
        state.Action.Should().NotBeNull();
        state.Action!.Type.Should().Be(ActionType.Function);
        state.Action.Name.Should().Be(action1Name);
        state.Action.Function.Should().NotBeNull();
        state.Action.Function!.RefName.Should().Be(functionName);
    }

    [Fact]
    public void Build_EventState_Should_Work()
    {
        //arrange
        var event1Name = "fake-event-1";
        var event2Name = "fake-event-2";
        var event3Name = "fake-event-3";
        var action1Name = "fake-action-1";
        var action2Name = "fake-action-2";
        var function1Name = "fake-function-1";
        var function2Name = "fake-function-2";
        var waitDuration = TimeSpan.FromHours(1.5);
        var stateBuilder = new EventStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (EventStateDefinition)stateBuilder
            .TriggeredBy(trigger => trigger
                .On(event1Name)
                .Execute(action1Name, action => action.Invoke(function => function.OfType(FunctionType.Expression).WithName(function1Name)))
                .Concurrently())
            .TriggeredBy(trigger => trigger
                .On(event2Name, event3Name)
                .Execute(action2Name, action => action.Invoke(function => function.OfType(FunctionType.Expression).WithName(function2Name)))
                .Sequentially())
            .WaitForAll()
            .For(waitDuration)
            .Build();

        //assert
        state.Should().NotBeNull();
        state.OnEvents.Should().HaveCount(2);

        state.OnEvents[0].EventRefs.Should().ContainSingle();
        state.OnEvents[0].EventRefs[0].Should().Be(event1Name);
        state.OnEvents[0].Actions.Should().ContainSingle();
        state.OnEvents[0].Actions[0].Type.Should().Be(ActionType.Function);
        state.OnEvents[0].Actions[0].Name.Should().Be(action1Name);
        state.OnEvents[0].Actions[0].Function.Should().NotBeNull();
        state.OnEvents[0].Actions[0].Function!.RefName.Should().Be(function1Name);

        state.OnEvents[1].EventRefs.Should().HaveCount(2);
        state.OnEvents[1].EventRefs.Should().Contain(new string[] { event2Name, event3Name });
        state.OnEvents[1].Actions.Should().ContainSingle();
        state.OnEvents[1].Actions[0].Type.Should().Be(ActionType.Function);
        state.OnEvents[1].Actions[0].Name.Should().Be(action2Name);
        state.OnEvents[1].Actions[0].Function.Should().NotBeNull();
        state.OnEvents[1].Actions[0].Function!.RefName.Should().Be(function2Name);
    }

    [Fact]
    public void Build_ExtensionState_Should_Work()
    {
        //arrange
        var metadata = new Dictionary<string, object>()
        {
            { "fake-property-1", "fake-property-1-value" },
            { "FakeProperty2", "fakeProperty2" },
            { "fake_Property_3", new { foo = new { bar = "baz" } } }
        };
        var type = "fake-state-type";
        var stateBuilder = new ExtensionStateBuilder(new PipelineBuilder(new WorkflowBuilder()), type);

        //act
        var state = stateBuilder
            .WithMetadata(metadata)
            .Build();

        //assert
        state.Should().NotBeNull();
        state.Type.Should().Be(type);
        state.Metadata.Should().BeEquivalentTo(metadata);
    }

    [Fact]
    public void Build_InjectState_Should_Work()
    {
        //arrange
        var stateBuilder = new InjectStateBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var data = new { foo = new { bar = "baz" } };

        //act
        var state = (InjectStateDefinition)stateBuilder
           .Data(data)
           .Build();

        //assert
        state.Should().NotBeNull();
        state.Data.Should().NotBeNull();
        state.Data.Should().Be(data);
    }

    [Fact]
    public void Build_OperationState_Should_Work()
    {
        //arrange
        var action1Name = "fake-action-1";
        var action2Name = "fake-action-2";
        var functionName = "fake-function";
        var eventName = "fake-event";
        var stateBuilder = new OperationStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (OperationStateDefinition)stateBuilder
            .Execute(action1Name, action => action.Invoke(function => function.OfType(FunctionType.Expression).WithName(functionName)))
            .Execute(action2Name, action => action.Consume(e => e.WithName(eventName).IsConsumed()))
            .Build();

        //assert
        state.Should().NotBeNull();

        state.Actions.Should().HaveCount(2);
        state.Actions[0].Type.Should().Be(ActionType.Function);
        state.Actions[0].Name.Should().Be(action1Name);
        state.Actions[0].Function.Should().NotBeNull();
        state.Actions[0].Function!.RefName.Should().Be(functionName);

        state.Actions[1].Type.Should().Be(ActionType.Event);
        state.Actions[1].Name.Should().Be(action2Name);
        state.Actions[1].Event.Should().NotBeNull();
        state.Actions[1].Event!.TriggerEventRef.Should().Be(eventName);
    }

    [Fact]
    public void Build_ParallelState_Should_Work()
    {
        //arrange
        var branch1Name = "fake-branch-1";
        var branch2Name = "fake-branch-2";
        var action1Name = "fake-action-1";
        var action2Name = "fake-action-2";
        var functionName = "fake-function";
        var eventName = "fake-event";
        var stateBuilder = new ParallelStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (ParallelStateDefinition)stateBuilder
            .Branch(branch1Name, branch => branch
                .Execute(action1Name, action => action.Invoke(function => function.OfType(FunctionType.Expression).WithName(functionName))))
            .Branch(branch2Name, branch => branch
                .Execute(action2Name, action => action.Consume(e => e.WithName(eventName).IsConsumed())))
            .Build();

        //assert
        state.Should().NotBeNull();
        state.Branches.Should().HaveCount(2);

        state.Branches[0].Actions.Should().ContainSingle();
        state.Branches[0].Actions[0].Type.Should().Be(ActionType.Function);
        state.Branches[0].Actions[0].Name.Should().Be(action1Name);
        state.Branches[0].Actions[0].Function.Should().NotBeNull();
        state.Branches[0].Actions[0].Function!.RefName.Should().Be(functionName);

        state.Branches[1].Actions.Should().ContainSingle();
        state.Branches[1].Actions[0].Type.Should().Be(ActionType.Event);
        state.Branches[1].Actions[0].Name.Should().Be(action2Name);
        state.Branches[1].Actions[0].Event.Should().NotBeNull();
        state.Branches[1].Actions[0].Event!.TriggerEventRef.Should().Be(eventName);
    }

    [Fact]
    public void Build_SleepState_Should_Work()
    {
        //arrange
        var duration = TimeSpan.FromSeconds(20);
        var stateBuilder = new SleepStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (SleepStateDefinition)stateBuilder
            .For(duration)
            .Build();

        //assert
        state.Should().NotBeNull();
        state.Duration.Should().Be(duration);
    }

    [Fact]
    public void Build_SwitchDataState_Should_Work()
    {
        //arrange
        var case1Name = "fake-data-case-1";
        var case1Condition = "${ true }";
        var case2Condition = "${ false }";
        var case1TransitionTo = "fake-state-1";
        var case2Name = "fake-data-case-1";
        var defaultCaseName = "fake-default-case";
        var defaultCaseTransitionTo = "fake-state-2";
        var stateBuilder = new SwitchStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (SwitchStateDefinition)stateBuilder
            .SwitchData()
            .WithCase(case1Name, switchCase => switchCase
                .When(case1Condition)
                .TransitionTo(case1TransitionTo))
            .WithCase(case2Name, switchCase => switchCase
                .When(case2Condition)
                .End())
            .WithDefaultCase(defaultCaseName, outcome => outcome.TransitionTo(defaultCaseTransitionTo))
            .Build();

        //assert
        state.Should().NotBeNull();
        state.SwitchType.Should().Be(SwitchStateType.Data);
        state.DataConditions.Should().HaveCount(2);

        state.DataConditions![0].Name.Should().Be(case1Name);
        state.DataConditions[0].OutcomeType.Should().Be(SwitchCaseOutcomeType.Transition);
        state.DataConditions![0].Condition.Should().Be(case1Condition);
        state.DataConditions[0].Transition.Should().NotBeNull();
        state.DataConditions[0].Transition!.NextState.Should().Be(case1TransitionTo);

        state.DataConditions![1].Name.Should().Be(case2Name);
        state.DataConditions[1].OutcomeType.Should().Be(SwitchCaseOutcomeType.End);
        state.DataConditions![1].Condition.Should().Be(case2Condition);
        state.DataConditions[1].End.Should().NotBeNull();

        state.DefaultCondition.Should().NotBeNull();
        state.DefaultCondition.Name.Should().Be(defaultCaseName);
        state.DefaultCondition.OutcomeType.Should().Be(SwitchCaseOutcomeType.Transition);
        state.DefaultCondition.Transition.Should().NotBeNull();
        state.DefaultCondition.Transition!.NextState.Should().Be(defaultCaseTransitionTo);
    }

    [Fact]
    public void Build_SwitchEventState_Should_Work()
    {
        //arrange
        var case1Name = "fake-event-case-1";
        var case1Event = "fake-event-1";
        var case2Event = "fake-event-2";
        var case1TransitionTo = "fake-state-1";
        var case2Name = "fake-event-case-1";
        var defaultCaseName = "fake-default-case";
        var defaultCaseTransitionTo = "fake-state-2";
        var stateBuilder = new SwitchStateBuilder(new PipelineBuilder(new WorkflowBuilder()));

        //act
        var state = (SwitchStateDefinition)stateBuilder
            .SwitchEvents()
            .WithCase(case1Name, switchCase => switchCase
                .On(case1Event)
                .TransitionTo(case1TransitionTo))
            .WithCase(case2Name, switchCase => switchCase
                .On(case2Event)
                .End())
            .WithDefaultCase(defaultCaseName, outcome => outcome.TransitionTo(defaultCaseTransitionTo))
            .Build();

        //assert
        state.Should().NotBeNull();
        state.SwitchType.Should().Be(SwitchStateType.Event);
        state.EventConditions.Should().HaveCount(2);

        state.EventConditions![0].Name.Should().Be(case1Name);
        state.EventConditions[0].OutcomeType.Should().Be(SwitchCaseOutcomeType.Transition);
        state.EventConditions[0].EventRef.Should().Be(case1Event);
        state.EventConditions[0].Transition.Should().NotBeNull();
        state.EventConditions[0].Transition!.NextState.Should().Be(case1TransitionTo);

        state.EventConditions![1].Name.Should().Be(case2Name);
        state.EventConditions[1].OutcomeType.Should().Be(SwitchCaseOutcomeType.End);
        state.EventConditions[1].EventRef.Should().Be(case2Event);
        state.EventConditions[1].End.Should().NotBeNull();

        state.DefaultCondition.Should().NotBeNull();
        state.DefaultCondition.Name.Should().Be(defaultCaseName);
        state.DefaultCondition.OutcomeType.Should().Be(SwitchCaseOutcomeType.Transition);
        state.DefaultCondition.Transition.Should().NotBeNull();
        state.DefaultCondition.Transition!.NextState.Should().Be(defaultCaseTransitionTo);
    }

    [Fact]
    public void Build_Function_Should_Work()
    {
        //arrange
        var builder = new FunctionBuilder(new WorkflowBuilder());
        var type = FunctionType.OData;
        var name = "fake-name";
        var metadata = new Dictionary<string, object>() { { "fake-extension-property", new { foo = new { bar = "baz" } } } };
        var operation = "https://tests.sdk-net.serverlessworkflow.io#fake-operation";
        var authentication = "fake-authentication";

        //act
        var function = builder
            .OfType(type)
            .WithName(name)
            .WithMetadata(metadata)
            .ForOperation(operation)
            .UseAuthentication(authentication)
            .Build();

        //assert
        function.Should().NotBeNull();
        function.Type.Should().Be(type);
        function.Name.Should().Be(name);
        function.Metadata.Should().BeEquivalentTo(metadata);
        function.Operation.Should().Be(operation);
        function.AuthRef.Should().Be(authentication);
    }

    [Fact]
    public void Build_Event_Should_Work()
    {
        //arrange
        var builder = new EventBuilder();
        var name = "fake-name";
        var metadata = new Dictionary<string, object>() { { "fake-extension-property", new { foo = new { bar = "baz" } } } };
        var source = new Uri("https://tests.sdk-net.serverlessworkflow.io#fake-operation");
        var type = "fake-type";
        var correlations = new Dictionary<string, string>() { { "fakeAttribute", "fakeAttributeValue" } };

        //act
        var e = builder
            .WithName(name)
            .WithMetadata(metadata)
            .WithSource(source)
            .WithType(type)
            .IsConsumed()
            .CorrelateUsing(correlations)
            .Build();

        //assert
        e.Should().NotBeNull();
        e.Type.Should().Be(type);
        e.Name.Should().Be(name);
        e.Metadata.Should().BeEquivalentTo(metadata);
        e.Source.Should().Be(source);
        e.Type.Should().Be(type);
        e.Correlations.Should().BeEquivalentTo(correlations.Select(kvp => new EventCorrelationDefinition(kvp.Key, kvp.Value)));
    }

    [Fact]
    public void Build_RetryStrategy_Should_Work()
    {
        //arrange
        var builder = new RetryStrategyBuilder();
        var name = "fake-name";
        var maxAttempts = 5u;
        var delay = TimeSpan.FromSeconds(3);
        var maxDelay = TimeSpan.FromSeconds(30);
        var delayIncrement = TimeSpan.FromSeconds(1);
        var delayMultiplier = 2;
        var jitterDuration = TimeSpan.FromSeconds(2);

        //act
        var strategy = builder
            .WithName(name)
            .MaxAttempts(maxAttempts)
            .WithDelayOf(delay)
            .WithMaxDelay(maxDelay)
            .WithDelayIncrement(delayIncrement)
            .WithDelayMultiplier(delayMultiplier)
            .WithJitterDuration(jitterDuration)
            .Build();

        //assert
        strategy.Should().NotBeNull();
        strategy.Name.Should().Be(name);
        strategy.MaxAttempts.Should().Be(maxAttempts);
        strategy.Delay.Should().Be(delay);
        strategy.MaxDelay.Should().Be(maxDelay);
        strategy.Increment.Should().Be(delayIncrement);
        strategy.Multiplier.Should().Be(delayMultiplier);
        strategy.JitterDuration.Should().Be(jitterDuration);
    }

    [Fact]
    public void Build_ErrorHandler_Should_Work()
    {
        //arrange
        var builder = new ErrorHandlerBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var retryStrategy = "fake-retry-strategy";
        var error = "fake-error";
        var errorCode = "fake-error-code";
        var nextState = "fake-next-state";

        //act
        var handler = builder
            .Catch(error, errorCode)
            .Retry(retryStrategy)
            .Then(flow => flow.TransitionTo(nextState))
            .Build();

        //assert
        handler.Should().NotBeNull();
        handler.Error.Should().Be(error);
        handler.Code.Should().Be(errorCode);
        handler.RetryRef.Should().Be(retryStrategy);
        handler.Transition.Should().NotBeNull();
        handler.Transition!.NextState.Should().Be(nextState);
    }

    [Fact]
    public void Build_EventAction_Should_Work()
    {
        //arrange
        var builder = new ActionBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var name = "fake-name";
        var condition = "${ true }";
        var consumeEventRef = "fake-consume-event-ref";
        var produceEventRef = "fake-produce-event-ref";
        var contextAttributes = new Dictionary<string, string>() { { "fake-attribute-1", "fake-attribute-1-value" } };

        //act
        var action = builder
            .WithName(name)
            .WithCondition(condition)
            .Consume(consumeEventRef)
            .ThenProduce(produceEventRef)
            .WithContextAttributes(contextAttributes)
            .Build();

        //assert
        action.Should().NotBeNull();
        action.Name.Should().Be(name);
        action.Condition.Should().Be(condition);
        action.Type.Should().Be(ActionType.Event);
        action.Event.Should().NotBeNull();
        action.Event!.TriggerEventRef.Should().Be(consumeEventRef);
        action.Event.ResultEventRef.Should().Be(produceEventRef);
        action.Event.ContextAttributes.Should().BeEquivalentTo(contextAttributes);
    }

    [Fact]
    public void Build_FunctionAction_Should_Work()
    {
        //arrange
        var builder = new ActionBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var name = "fake-name";
        var condition = "${ true }";
        var function = "fake-function";
        var arguments = new Dictionary<string, object>() { };

        //act
        var action = builder
            .WithName(name)
            .WithCondition(condition)
            .Invoke(function)
            .WithArguments(arguments)
            .Build();

        //assert
        action.Should().NotBeNull();
        action.Name.Should().Be(name);
        action.Condition.Should().Be(condition);
        action.Type.Should().Be(ActionType.Function);
        action.Function.Should().NotBeNull();
        action.Function!.RefName.Should().Be(function);
        action.Function.Arguments.Should().BeEquivalentTo(arguments);
    }

    [Fact]
    public void Build_SubflowAction_Should_Work()
    {
        //arrange
        var builder = new ActionBuilder(new PipelineBuilder(new WorkflowBuilder()));
        var name = "fake-name";
        var condition = "${ true }";
        var workflowId = "fake-workflow-id";
        var workflowVersion = "0.1.0";
        var invocationMode = InvocationMode.Asynchronous;

        //act
        var action = builder
            .WithName(name)
            .WithCondition(condition)
            .Run(workflowId, workflowVersion, invocationMode)
            .Build();

        //assert
        action.Should().NotBeNull();
        action.Should().NotBeNull();
        action.Name.Should().Be(name);
        action.Condition.Should().Be(condition);
        action.Type.Should().Be(ActionType.Subflow);
        action.Subflow.Should().NotBeNull();
        action.Subflow!.WorkflowId.Should().Be(workflowId);
        action.Subflow.Version.Should().Be(workflowVersion);
    }

}
