using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantRepository : Generic.TRepository<GroupChatParticipant>
    {
        public GroupChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChatParticipant entity, CancellationToken cancellationToken = default)
        {
            GroupChat? gc = await _dbContext.Set<GroupChat>().
                FirstOrDefaultAsync(
                x => x.Id == entity.GroupChatId
                , cancellationToken);
            if (gc == null) return Result.Failure(new("Error.NullValue"/*"Error.GroupChatDoesNotExist"*/, "Групповой чат не существует."));
            if (gc.IsPublic) return Result.Failure(new("Error.AttemptToAddToPublicChat"/*"Error.GroupChatDoesNotExist"*/, "Попытка добавления в публичный чат"));
            await _dbContext.Set<GroupChatParticipant>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
