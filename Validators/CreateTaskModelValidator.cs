using FluentValidation;
using TaskManager.Models;

namespace TaskManager.Validators
{
    public class CreateTaskModelValidator : AbstractValidator<CreateTaskModel>
    {
        public CreateTaskModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 charactrers");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.PriorityId)
                .GreaterThan(0).WithMessage("Priority id is required");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category id is required");
        }
    }
}
