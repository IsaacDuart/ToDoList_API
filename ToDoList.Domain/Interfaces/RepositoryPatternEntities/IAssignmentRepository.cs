using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.RepositoryPatternEntities;

public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    Task<IEnumerable<Assignment>> GetUnfinishedAssignments();
}