using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupInviteRepository : Generic.TRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<GroupInvite>> GetByUserAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .AsNoTracking()
                .Where(gi => gi.GroupId == group.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<GroupInvite>> GetForUserAsync(User receiver, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .AsNoTracking()
                .Where(gi => gi.ReceiverId == receiver.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<GroupInvite>> GetFromUserAsync(User sender, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .AsNoTracking()
                .Where(gi => gi.SenderId == sender.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
