using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate a workflow's <see cref="StateDefinition"/>s
    /// </summary>
    public class WorkflowStatesValidator
        : PropertyValidator
    {

        private static Dictionary<Type, IEnumerable<Type>> StateValidatorTypes = typeof(WorkflowDefinitionValidator).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && !t.IsGenericType && t.IsClass && t.GetGenericType(typeof(StateDefinitionValidator<>)) != null)
            .GroupBy(t => t.GetGenericType(typeof(StateDefinitionValidator<>)).GetGenericArguments().First())
            .ToDictionary(g => g.Key, g => g.AsEnumerable());

        /// <summary>
        /// Initializes a new <see cref="WorkflowStatesValidator"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public WorkflowStatesValidator(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            List<StateDefinition> states = (List<StateDefinition>)context.PropertyValue;
            int index = 0;
            foreach (StateDefinition state in states)
            {
                if (!StateValidatorTypes.TryGetValue(state.GetType(), out IEnumerable<Type> validatorTypes))
                    continue;
                IEnumerable<IValidator> validators = validatorTypes.Select(t => (IValidator)Activator.CreateInstance(t, (WorkflowDefinition)context.InstanceToValidate));
                foreach (IValidator validator in validators)
                {
                    object[] args = new object[] { state };
                    MethodInfo validationMethod = typeof(IValidator<>).MakeGenericType(state.GetType()).GetMethods().Single(m => m.Name == nameof(IValidator.Validate) && m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType != typeof(IValidationContext));
                    ValidationResult validationResult = (ValidationResult)validationMethod.Invoke(validator, args);
                    if (validationResult.IsValid)
                        continue;
                    this.ErrorCode = $"{nameof(WorkflowDefinition.States)}[{index}]";
                    this.SetErrorMessage(string.Join(Environment.NewLine, validationResult.Errors.Select(e => $"{e.ErrorCode}: {e.ErrorMessage}")));
                    return false;
                }
                index++;
            }
            return true;
        }

    }

}
