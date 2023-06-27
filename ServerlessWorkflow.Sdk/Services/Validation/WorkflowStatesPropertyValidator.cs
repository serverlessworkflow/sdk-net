using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation;


/// <summary>
/// Represents the service used to validate a workflow's state definitions
/// </summary>
internal class WorkflowStatesPropertyValidator
    : PropertyValidator<WorkflowDefinition, List<StateDefinition>>
{

    private static readonly Dictionary<Type, IEnumerable<Type>> StateValidatorTypes = typeof(WorkflowDefinitionValidator).Assembly.GetTypes()
        .Where(t => !t.IsAbstract && !t.IsInterface && !t.IsGenericType && t.IsClass && t.GetGenericType(typeof(StateDefinitionValidator<>)) != null)
        .GroupBy(t => t.GetGenericType(typeof(StateDefinitionValidator<>))!.GetGenericArguments().First())
        .ToDictionary(g => g.Key, g => g.AsEnumerable());

    /// <summary>
    /// Initializes a new <see cref="WorkflowStatesPropertyValidator"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    public WorkflowStatesPropertyValidator(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public override string Name => nameof(WorkflowStatesPropertyValidator);

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    protected override string GetDefaultMessageTemplate(string errorCode) => "Failed to validate the state";
    
    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<WorkflowDefinition> context, List<StateDefinition> value)
    {
        var index = 0;
        foreach (var state in value)
        {
            if (!StateValidatorTypes.TryGetValue(state.GetType(), out var validatorTypes)) continue;
            var validators = validatorTypes!.Select(t => (IValidator)Activator.CreateInstance(t, context.InstanceToValidate)!);
            foreach (IValidator validator in validators)
            {
                var args = new object[] { state };
                var validationMethod = typeof(IValidator<>).MakeGenericType(state.GetType())
                    .GetMethods()
                    .Single(m => 
                        m.Name == nameof(IValidator.Validate) 
                        && m.GetParameters().Length == 1 
                        && m.GetParameters().First().ParameterType != typeof(IValidationContext));
                var validationResult = (FluentValidation.Results.ValidationResult)validationMethod.Invoke(validator, args)!;
                if (validationResult.IsValid) continue;
                foreach (var failure in validationResult.Errors)
                {
                    failure.PropertyName = $"{nameof(WorkflowDefinition.States)}[{index}].{failure.PropertyName}";
                    context.AddFailure(failure);
                }
                return false;
            }
            index++;
        }
        return true;
    }

}
