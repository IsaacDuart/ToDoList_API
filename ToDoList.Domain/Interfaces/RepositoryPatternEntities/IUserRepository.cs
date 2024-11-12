using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.RepositoryPatternEntities;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task<IEnumerable<User>> SearchByName(string name);
}