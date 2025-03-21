
using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Persistence.Repository
{
    class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<UserInGroup>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .AsNoTracking()
                .Where(uig => uig.GroupId == group.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<UserInGroup>> GetByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .AsNoTracking()
                .Where(uig => uig.UserId == user.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
