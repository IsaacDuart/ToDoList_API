using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Interfaces.RepositoryPatternEntities;

public interface IBaseRepository<T> where T : Base
{
    //CRUD
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task Delete(long id);
    Task<T?> GetById(long id);
    Task<IEnumerable<T>> GetAll();
}