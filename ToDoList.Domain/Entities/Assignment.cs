using ToDoList.Domain.Exception;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class Assignment : Base
{
    public string Description { get; private set; }
    public bool Conclued { get; private set; } = false;
    public DateTime ConcluedAt { get; private set; }
    public DateTime DeadLine { get; private set; }
    public long UserId { get; private set; }
    public long AssignmentListId { get; private set; }
    
    //Ef
    public virtual User User { get; private set; }
    public virtual AssignmentList AssignmentList { get; private set; }

    public Assignment(string description, long userId, long assignmentListId, DateTime deadLine)
    {
        Description = description;
        UserId = userId;
        AssignmentListId = assignmentListId;
        DeadLine = deadLine;
        Validate();
    }

    public void ChangeDescription(string description)
    {
        Description = description;
        Validate();
    }

    public void FinishTask()
    {
        Conclued = true;
        ConcluedAt = DateTime.Now;
    }
    
    public override bool Validate()
    {
        var validator = new AssignmentValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                _errors.Add(error.ErrorMessage);
            }
            throw new DomainException("Something went wrong", _errors);
        }

        return true;
    }
}