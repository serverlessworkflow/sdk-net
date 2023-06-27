using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.Services.Validation;


/// <summary>
/// Represents the service used to validate a workflow's <see cref="ICollection{T}"/>s
/// </summary>
public class CollectionPropertyValidator<TElement>
    : PropertyValidator<WorkflowDefinition, IEnumerable<TElement>?>
{

    /// <summary>
    /// Initializes a new <see cref="CollectionPropertyValidator{TElement}"/>
    /// </summary>
    /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
    public CollectionPropertyValidator(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public override string Name => "CollectionValidator";

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <inheritdoc/>
    public override bool IsValid(ValidationContext<WorkflowDefinition> context, IEnumerable<TElement>? value)
    {
        var index = 0;
        if (value == null) return true;
        foreach (TElement elem in value)
        {
            IEnumerable<IValidator<TElement>> validators = this.ServiceProvider.GetServices<IValidator<TElement>>();
            foreach (IValidator<TElement> validator in validators)
            {
                FluentValidation.Results.ValidationResult validationResult = validator.Validate(elem);
                if (validationResult.IsValid) continue;
                foreach (var failure in validationResult.Errors) context.AddFailure(failure);
                return false;
            }
            index++;
        }
        return true;
    }

}
