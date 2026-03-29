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
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .When(x => x.Name != null);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .When(x => x.Description != null);

            RuleFor(x => x.PriorityId)
                .GreaterThan(0)
                .When(x => x.PriorityId != null);
        }
    }
}
