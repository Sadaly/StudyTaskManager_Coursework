using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatMessageRepository : Generic.TRepository<GroupChatMessage>, IGroupChatMessageRepository
    {
        public GroupChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }
        public override async Task<Result> AddAsync(GroupChatMessage entity, CancellationToken cancellationToken = default)
        {
            // проверка на существование чата
            GroupChat? gc = await _dbContext.Set<GroupChat>().FirstOrDefaultAsync(x => x.Id == entity.GroupChatId, cancellationToken);

            if (gc == null) return Result.Failure(new(
                $"{nameof(GroupChatMessage)}.{nameof(GroupChat)}NullValue",
                "Групповой чат не существует."));

            // проверка на причастность к чату
            if (!gc.IsPublic)
            {
                GroupChatParticipant? gcp =
                    await _dbContext.Set<GroupChatParticipant>().
                    FirstOrDefaultAsync(x => x.GroupChatId == entity.GroupChatId && x.UserId == entity.SenderId, cancellationToken);

                if (gcp == null) return Result.Failure(new Error(
                    $"{nameof(GroupChatMessage)}.GroupChatParticipantNullValue",
                    "Пользователь не принадлежит чату."));
            }
            await _dbContext.Set<GroupChatMessage>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
