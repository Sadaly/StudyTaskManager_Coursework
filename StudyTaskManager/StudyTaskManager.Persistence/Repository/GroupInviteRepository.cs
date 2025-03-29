using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupInviteRepository : Generic.TRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupInvite groupInvite, CancellationToken cancellationToken = default)
        {
            // Проверка наличия пользователя в чате при попытке добавить приглашение
            UserInGroup? uig = await _dbContext.Set<UserInGroup>()
                .FirstOrDefaultAsync(uig => uig.GroupId == groupInvite.GroupId && uig.UserId == groupInvite.ReceiverId, cancellationToken: cancellationToken);

            if (uig != null)
                return Result.Failure(new Error(
                    $"{typeof(UserInGroup)}.TheUserIsAlreadyInTheGroup",
                    $"Пользователь(" +
                    $"{(groupInvite.Receiver == null ?
                        groupInvite.ReceiverId :
                        groupInvite.Receiver.Username)}" +
                    $") уже в группе(" +
                    $"{(groupInvite.Group == null ?
                        groupInvite.GroupId :
                        groupInvite.Group.Title)}"));

            await _dbContext.Set<GroupInvite>().AddAsync(groupInvite, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result<List<GroupInvite>>> GetByUserAsync(Group group, CancellationToken cancellationToken = default)
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
