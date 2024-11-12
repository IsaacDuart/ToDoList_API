using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;
using ToDoList.Infra.Context;

namespace ToDoList.Infra.Repositories;

public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    private readonly AppDbContext _db;
    public AssignmentRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Assignment>> GetUnfinishedAssignments() =>
        await _db.Assignments
            .AsNoTracking()
            .Where(x => !x.Conclued)
            .ToListAsync();
}