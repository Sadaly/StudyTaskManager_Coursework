using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRoleRepository : Generic.TWithIdRepository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupRole>>> GetByGroupAsync(Group group, bool togetherWithTheGeneral, CancellationToken cancellationToken = default)
        {
            if (togetherWithTheGeneral)
            {
                return await _dbContext.Set<GroupRole>()
                    .Where(gr => gr.GroupId == group.Id || gr.GroupId == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            return await _dbContext.Set<GroupRole>()
                .Where(gr => gr.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupRole>>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                .Where(gr => gr.GroupId == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
