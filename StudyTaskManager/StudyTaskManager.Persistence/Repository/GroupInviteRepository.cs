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
        // TODO добавить проверку наличия пользователя в чате при попытке добавить приглашение
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
