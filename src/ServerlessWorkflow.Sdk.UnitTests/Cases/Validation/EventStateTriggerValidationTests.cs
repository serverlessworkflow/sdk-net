using FluentAssertions;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{

    public class EventStateTriggerValidationTests
    {

        [Fact]
        public void Validate_EventStateTrigger_NoEvents_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .StartsWith("fake", flow => flow.Events())
                .End()
                .Build();
            var state = new EventStateDefinition();
            var trigger = new EventStateTriggerDefinition();

            //act
            var result = new EventStateTriggerDefinitionValidator(workflow, state).Validate(trigger);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(EventStateTriggerDefinition.Events));
        }

    }

}
