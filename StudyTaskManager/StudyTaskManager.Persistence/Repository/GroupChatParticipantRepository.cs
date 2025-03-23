using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantRepository : Generic.TRepository<GroupChatParticipant>
    {
        public GroupChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(GroupChatParticipant entity, CancellationToken cancellationToken = default)
        {
            GroupChat? gc = await _dbContext.Set<GroupChat>().FirstOrDefaultAsync(x => x.Id == entity.GroupChatId, cancellationToken);

            if (gc == null) throw new Exception("Несуществующий чат");

            if (gc.IsPublic) throw new Exception("Попытка добавления в публичнй чат публичный");

            await _dbContext.Set<GroupChatParticipant>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
