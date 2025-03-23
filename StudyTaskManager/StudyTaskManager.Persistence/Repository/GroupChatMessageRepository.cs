using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatMessageRepository : Generic.TRepository<GroupChatMessage>
    {
        public GroupChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }
        public override async Task AddAsync(GroupChatMessage entity, CancellationToken cancellationToken = default)
        {
            GroupChatParticipant? gcp =
                await _dbContext.Set<GroupChatParticipant>().
                FirstOrDefaultAsync(
                    x =>
                        x.GroupChatId == entity.GroupChatId &&
                        x.UserId == entity.SenderId
                    , cancellationToken);

            if (gcp == null) throw new Exception("Пользователь не принадлежит чату");

            await _dbContext.Set<GroupChatMessage>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
