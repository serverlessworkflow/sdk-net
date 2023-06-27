using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class EventReferenceValidationTests
{

    [Fact]
    public void Validate_EventReference_TriggerEventNotSet_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
           .StartsWith("fake", flow => flow.Callback())
           .End()
           .Build();
        var eventRef = new EventReference();

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEventRef));
    }

    [Fact]
    public void Validate_EventReference_TriggerEventNotFound_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
           .StartsWith("fake", flow => flow.Callback())
           .End()
           .Build();
        var eventRef = new EventReference() { TriggerEventRef = "fake" };

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEventRef));
    }

    [Fact]
    public void Validate_EventReference_TriggerEventConsumed_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .AddEvent(new EventDefinition() { Kind = EventKind.Consumed, Name = "fake" })
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();
        var eventRef = new EventReference() { TriggerEventRef = "fake" };

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEventRef));
    }

    [Fact]
    public void Validate_EventReference_ResultEventNotSet_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();
        var eventRef = new EventReference() { TriggerEventRef = "fake" };

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEventRef));
    }

    [Fact]
    public void Validate_EventReference_ResultEventNotFound_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();
        var eventRef = new EventReference() { TriggerEventRef = "fakeTrigger", ResultEventRef = "fakeResult" };

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEventRef));
    }

    [Fact]
    public void Validate_EventReference_ResultEventProduced_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .AddEvent(new EventDefinition() { Kind = EventKind.Produced, Name = "fakeResult" })
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();
        var eventRef = new EventReference() { TriggerEventRef = "fakeTrigger", ResultEventRef = "fakeResult" };

        //act
        var result = new EventReferenceValidator(workflow).Validate(eventRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEventRef));
    }

}
