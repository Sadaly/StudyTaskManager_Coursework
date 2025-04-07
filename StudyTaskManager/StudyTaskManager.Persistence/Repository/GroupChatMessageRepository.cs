using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatMessageRepository : Generic.TRepository<GroupChatMessage>, IGroupChatMessageRepository
    {
        public GroupChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChatMessage groupChatMessage, CancellationToken cancellationToken = default)
        {
            GroupChat? groupChat = await _dbContext.Set<GroupChat>().FirstOrDefaultAsync(gc => gc.Id == groupChatMessage.GroupChatId, cancellationToken);
            if (groupChat == null) return Result.Failure(PersistenceErrors.GroupChat.NotFound);

            if (!groupChat.IsPublic)
            {
                GroupChatParticipant? groupChatParticipant =
                    await _dbContext.Set<GroupChatParticipant>().
                    FirstOrDefaultAsync(
                        gcp =>
                            gcp.GroupChatId == groupChatMessage.GroupChatId &&
                            gcp.UserId == groupChatMessage.SenderId
                        , cancellationToken);
                if (groupChatParticipant == null) return Result.Failure(PersistenceErrors.GroupChatParticipant.UserDoesNotBelongToTheGroupChat);
            }

            await _dbContext.Set<GroupChatMessage>().AddAsync(groupChatMessage, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
