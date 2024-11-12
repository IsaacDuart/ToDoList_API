using ToDoList.Domain.Validators;
using System;
using ToDoList.Domain.Exception;

namespace ToDoList.Domain.Entities;

public class AssignmentList : Base
{
    public string Name { get; private set; }
    public long UserId { get; private set; }
    
    //Ef
    public virtual User User { get; private set; }
    public virtual List<Assignment> Assignments { get; private set; }

    public AssignmentList(string name, long userId)
    {
        Name = name;
        UserId = userId;
        Assignments = new List<Assignment>();
    }

    public override bool Validate()
    {
        var validator = new AssignmentListValidator();
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