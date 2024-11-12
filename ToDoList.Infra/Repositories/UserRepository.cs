using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;
using ToDoList.Infra.Context;

namespace ToDoList.Infra.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly AppDbContext _db;
    
    public UserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<User?> GetByEmail(string email) => await _db.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<IEnumerable<User>> SearchByName(string name) => await _db.Users.AsNoTracking()
        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
        .ToListAsync();
}