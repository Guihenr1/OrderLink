using FluentValidation;

namespace OrderLink.Sync.Kitchen.Domain.Entities.Validations
{
    public class DishValidation : AbstractValidator<Dish>
    {
        public DishValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The Name field is required")
                .Length(2, 100).WithMessage("The Name field must have between 2 and 100 characters");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("The Description field is required")
                .Length(2, 255).WithMessage("The Description field must have between 2 and 255 characters");
        }
    }
}
