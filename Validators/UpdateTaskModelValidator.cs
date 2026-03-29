using FluentValidation;
using TaskManager.Models;

namespace TaskManager.Validators
{
    public class UpdateTaskModelValidator : AbstractValidator<UpdateTaskModel>
    {
        public UpdateTaskModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.PriorityId)
                .GreaterThan(0).WithMessage("Priority id is required");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category id is required");
        }
    }
}
