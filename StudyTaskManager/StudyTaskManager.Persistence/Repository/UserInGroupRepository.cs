
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<UserInGroup>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserInGroup>> GetByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
