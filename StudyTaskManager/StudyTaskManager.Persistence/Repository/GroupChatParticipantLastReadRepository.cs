using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantLastReadRepository : Generic.TRepository<GroupChatParticipantLastRead>, IGroupChatParticipantLastReadRepository
    {
        public GroupChatParticipantLastReadRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupChatParticipantLastRead groupChatParticipantLastRead, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(u => u.Id == groupChatParticipantLastRead.UserId, cancellationToken);
            if (user == null) return Result.Failure(PersistenceErrors.User.NotFound);

            GroupChat? groupChat = await _dbContext.Set<GroupChat>()
                .FirstOrDefaultAsync(gc => gc.Id == groupChatParticipantLastRead.GroupChatId, cancellationToken);
            if (groupChat == null) return Result.Failure(PersistenceErrors.GroupChat.NotFound);

            GroupChatMessage? groupChatMessage = await _dbContext.Set<GroupChatMessage>()
                .FirstOrDefaultAsync(gcm => gcm.Ordinal == groupChatParticipantLastRead.LastReadMessageId, cancellationToken);
            if (groupChatMessage == null) return Result.Failure(PersistenceErrors.GroupChatMessage.NotFound);

            // TODO проверка наличия в чате пользователя

            await _dbContext.Set<GroupChatParticipantLastRead>().AddAsync(groupChatParticipantLastRead, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
