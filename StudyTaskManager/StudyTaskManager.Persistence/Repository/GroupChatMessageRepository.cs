using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatMessageRepository : Generic.TRepository<GroupChatMessage>
    {
        public GroupChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }
        public override async Task<Result> AddAsync(GroupChatMessage entity, CancellationToken cancellationToken = default)
        {
            // проверка на существование чата
            GroupChat? gc = await _dbContext.Set<GroupChat>().
                FirstOrDefaultAsync(
                    x => x.Id == entity.GroupChatId
                    , cancellationToken);
            if (gc == null) return Result.Failure(new("Error.NullValue"/*"Error.GroupChatDoesNotExist"*/, "Групповой чат не существует."));
            // проверка на причастность к чату
            if (!gc.IsPublic)
            {
                GroupChatParticipant? gcp =
                    await _dbContext.Set<GroupChatParticipant>().
                    FirstOrDefaultAsync(
                        x =>
                            x.GroupChatId == entity.GroupChatId &&
                            x.UserId == entity.SenderId
                        , cancellationToken);
                if (gcp == null) return Result.Failure(new Error("Error.NullValue"/*"Error.UserDoesNotBelongToChat"*/, "Пользователь не принадлежит чату."));
            }
            await _dbContext.Set<GroupChatMessage>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
