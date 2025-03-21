using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRoleRepository : Generic.TWithIdRepository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<GroupRole>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                .AsNoTracking()
                .Where(gr => gr.GroupId == group.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<GroupRole>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                .AsNoTracking()
                .Where(gr => gr.GroupId == null)
                .ToListAsync(cancellationToken);
        }
    }
}
