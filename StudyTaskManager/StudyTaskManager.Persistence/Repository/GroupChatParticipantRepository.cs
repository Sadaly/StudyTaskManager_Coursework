using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantRepository : Generic.TRepository<GroupChatParticipant>, IGroupChatParticipantRepository
    {
        public GroupChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChatParticipant entity, CancellationToken cancellationToken = default)
        {
            GroupChat? gc = await _dbContext.Set<GroupChat>().FirstOrDefaultAsync(x => x.Id == entity.GroupChatId, cancellationToken);

            if (gc == null) return Result.Failure(new(
                $"{nameof(GroupChatParticipant)}.{nameof(GroupChat)}NullValue",
                "Групповой чат не существует."));

            if (gc.IsPublic) return Result.Failure(new(
                $"{nameof(GroupChatParticipant)}.AttemptToAddToPublicChat",
                "Попытка добавления в публичный чат"));

            await _dbContext.Set<GroupChatParticipant>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
