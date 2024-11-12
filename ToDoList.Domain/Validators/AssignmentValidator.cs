using FluentValidation;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Validators;

public class AssignmentValidator : AbstractValidator<Assignment>
{
    public AssignmentValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(150).WithMessage("Description must be between 150 characters");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .NotNull().WithMessage("UserId is required");
        
        //Inserir if
        RuleFor(x => x.AssignmentListId)
            .NotEmpty().WithMessage("AssignmentListId is required")
            .NotNull().WithMessage("AssignmentListId is required");
        
        RuleFor(x => x.DeadLine)
            .NotEmpty().WithMessage("DeadLine is required")
            .NotNull().WithMessage("DeadLine is required")
            .Must(x => x >= DateTime.Now).WithMessage("DeadLine must be in the future");
        
    }
}