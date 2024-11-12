using FluentValidation;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Validators;

public class AssignmentListValidator : AbstractValidator<AssignmentList>
{
    public AssignmentListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .NotNull().WithMessage("UserId is required");
    }
}