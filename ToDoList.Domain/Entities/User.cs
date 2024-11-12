using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class User : Base
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; set; }
    
    //Ef
    public virtual List<AssignmentList> AssignmentLists { get; private set; }
    public virtual List<Assignment> Assignments { get; private set; }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Assignments = new List<Assignment>();
        Validate();
    }

    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangeEmail(string email)
    {
        Email = email;
        Validate();
    }

    public void ChangePassword(string password)
    {
        Password = password;
        Validate();
    }
    
    public override bool Validate()
    {
        var validator = new UserValidator();
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