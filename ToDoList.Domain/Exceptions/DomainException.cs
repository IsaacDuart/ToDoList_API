namespace ToDoList.Domain.Exception;
using System;

public class DomainException : Exception
{
    public IReadOnlyCollection<string> Errors { get;}

    public DomainException(string message, IReadOnlyCollection<string> errors) : base(message)
    {
        Errors = errors;
    }

    public DomainException(string message) : base(message)
    {
        
    }
}