using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;
using ToDoList.Infra.Context;

namespace ToDoList.Infra.Repositories;

public class AssignmentListRepository : BaseRepository<AssignmentList>, IAssignmentListRepository
{
    private readonly AppDbContext _db;
    public AssignmentListRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AssignmentList>> SearchByListName(string name) => await _db.AssignmentLists
        .AsNoTracking()
        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
        .ToListAsync();
}