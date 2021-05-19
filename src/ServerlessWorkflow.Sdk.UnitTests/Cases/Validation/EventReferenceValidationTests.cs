using FluentAssertions;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{
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

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEvent));
        }

        [Fact]
        public void Validate_EventReference_TriggerEventNotFound_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
               .StartsWith("fake", flow => flow.Callback())
               .End()
               .Build();
            var eventRef = new EventReference() { TriggerEvent = "fake" };

            //act
            var result = new EventReferenceValidator(workflow).Validate(eventRef);

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEvent));
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
            var eventRef = new EventReference() { TriggerEvent = "fake" };

            //act
            var result = new EventReferenceValidator(workflow).Validate(eventRef);

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.TriggerEvent));
        }

        [Fact]
        public void Validate_EventReference_ResultEventNotSet_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
               .StartsWith("fake", flow => flow.Callback())
               .End()
               .Build();
            var eventRef = new EventReference() { TriggerEvent = "fake" };

            //act
            var result = new EventReferenceValidator(workflow).Validate(eventRef);

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEvent));
        }

        [Fact]
        public void Validate_EventReference_ResultEventNotFound_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
               .StartsWith("fake", flow => flow.Callback())
               .End()
               .Build();
            var eventRef = new EventReference() { TriggerEvent = "fakeTrigger", ResultEvent = "fakeResult" };

            //act
            var result = new EventReferenceValidator(workflow).Validate(eventRef);

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEvent));
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
            var eventRef = new EventReference() { TriggerEvent = "fakeTrigger", ResultEvent = "fakeResult" };

            //act
            var result = new EventReferenceValidator(workflow).Validate(eventRef);

            //asset
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventReference.ResultEvent));
        }

    }

}
