using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRoleRepository : Generic.TWithIdRepository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<GroupRole>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupRole>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
