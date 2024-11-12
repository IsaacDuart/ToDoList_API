using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.RepositoryPatternEntities;

public interface IAssignmentListRepository : IBaseRepository<AssignmentList>
{
    Task<IEnumerable<AssignmentList>> SearchByListName(string name);
}