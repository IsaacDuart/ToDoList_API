using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Entities;

public abstract class Base : IEntityValidator
{
    public long Id { get; private set; }

    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;
    
    
    
    public abstract bool Validate();
}