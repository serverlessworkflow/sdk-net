using FluentValidation;
using FluentValidation.Validators;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the <see cref="PropertyValidator"/> used to validate a <see cref="FunctionDefinition"/> collection
/// </summary>
public class FunctionDefinitionCollectionValidator
    : PropertyValidator<WorkflowDefinition, IEnumerable<FunctionDefinition>>
{

    /// <inheritdoc/>
    public override string Name => "FunctionDefinitionCollection";

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<WorkflowDefinition> context, IEnumerable<FunctionDefinition> value)
    {
        var workflow = context.InstanceToValidate;
        var index = 0;
        var validator = new FunctionDefinitionValidator(workflow);
        foreach (var function in value)
        {

            var validationResult = validator.Validate(function);
            if (validationResult.IsValid)
            {
                index++;
                continue;
            }
            foreach(var failure in validationResult.Errors) context.AddFailure(failure);
            return false;
        }
        return true;
    }

}
