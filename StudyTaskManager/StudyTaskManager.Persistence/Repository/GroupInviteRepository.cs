using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupInviteRepository : Generic.TRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<GroupInvite>> GetByUserAsync(Group group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupInvite>> GetForUserAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupInvite>> GetFromUserAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
