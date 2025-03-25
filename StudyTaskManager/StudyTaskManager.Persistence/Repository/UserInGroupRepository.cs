
using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<UserInGroup>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<UserInGroup>>> GetByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.UserId == user.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
