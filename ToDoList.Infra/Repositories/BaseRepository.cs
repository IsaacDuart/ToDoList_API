using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;
using ToDoList.Infra.Context;

namespace ToDoList.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly AppDbContext _db;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<T> Create(T entity)
    {
        _db.Add(entity);
        await _db.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _db.Update(entity);
        await _db.SaveChangesAsync();
        
        return entity;
    }

    public async Task Delete(long id)
    {
        var entity = await _db.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new DomainException("Entity not found");
        }

        _db.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<T?> GetById(long id)
    {
        var entity = await _db.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return entity;
    } 

    public async Task<IEnumerable<T>> GetAll() => await _db.Set<T>().
        AsNoTracking()
        .ToListAsync();
}