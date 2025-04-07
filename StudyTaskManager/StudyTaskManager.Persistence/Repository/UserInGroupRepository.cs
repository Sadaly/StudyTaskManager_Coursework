using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(UserInGroup userInGroup, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userInGroup.UserId, cancellationToken);
            if (user == null) return Result.Failure(PersistenceErrors.User.NotFound);

            Group? group = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.Id == userInGroup.GroupId, cancellationToken);
            if (group == null) return Result.Failure(PersistenceErrors.Group.NotFound);

            GroupRole? groupRole = await _dbContext.Set<GroupRole>().FirstOrDefaultAsync(gr => gr.Id == userInGroup.RoleId, cancellationToken);
            if (groupRole == null) return Result.Failure(PersistenceErrors.GroupRole.NotFound);

            await _dbContext.Set<UserInGroup>().AddAsync(userInGroup, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<List<UserInGroup>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<UserInGroup?>> GetByUserAndGroupAsync(User user, Group group, CancellationToken cancellationToken = default)
        {
            UserInGroup? userInGroup = await _dbContext.Set<UserInGroup>()
                .FirstOrDefaultAsync(uig => uig.UserId == user.Id && uig.GroupId == group.Id, cancellationToken);
            return Result.Success(userInGroup);
        }

        public async Task<Result<UserInGroup?>> GetByUserAndGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            UserInGroup? userInGroup = await _dbContext.Set<UserInGroup>()
                .FirstOrDefaultAsync(uig => uig.UserId == userId && uig.GroupId == groupId, cancellationToken);
            return Result.Success(userInGroup);
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
