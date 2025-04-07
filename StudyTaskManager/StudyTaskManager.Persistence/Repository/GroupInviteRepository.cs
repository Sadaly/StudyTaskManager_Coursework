using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupInviteRepository : Generic.TRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupInvite groupInvite, CancellationToken cancellationToken = default)
        {
            UserInGroup? uig = await _dbContext.Set<UserInGroup>()
                .FirstOrDefaultAsync(uig => uig.GroupId == groupInvite.GroupId && uig.UserId == groupInvite.ReceiverId, cancellationToken);
            if (uig != null) return Result.Failure(PersistenceErrors.GroupInvite.UserIsAlreadyInTheGroup);

            GroupInvite? groupInvite_old = await _dbContext.Set<GroupInvite>()
                .FirstOrDefaultAsync(gi => gi.ReceiverId == groupInvite.ReceiverId && gi.GroupId == groupInvite.GroupId, cancellationToken);
            if (groupInvite_old != null) return Result.Failure(PersistenceErrors.GroupInvite.UserAlreadyHasAnInvitationToThisGroup);

            await _dbContext.Set<GroupInvite>().AddAsync(groupInvite, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result<List<GroupInvite>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupInvite>>> GetForUserAsync(User receiver, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.ReceiverId == receiver.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupInvite>>> GetFromUserAsync(User sender, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.SenderId == sender.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
