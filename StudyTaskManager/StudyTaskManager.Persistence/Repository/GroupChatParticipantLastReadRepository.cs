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

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupChatParticipantLastRead entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return user; }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return groupChat; }

            Result<GroupChatMessage> groupChatMessage = await GetFromDBAsync<GroupChatMessage>(gcm => gcm.Ordinal == entity.LastReadMessageId, PersistenceErrors.GroupChatMessage.NotFound, cancellationToken);
            if (groupChatMessage.IsFailure) { return groupChatMessage; }

            Result<GroupChatParticipantLastRead> groupChatParticipantLastRead = await GetFromDBAsync(
                gcplr =>
                    gcplr.LastReadMessageId == entity.LastReadMessageId &&
                    gcplr.GroupChatId == entity.GroupChatId &&
                    gcplr.UserId == entity.UserId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            if (groupChatParticipantLastRead.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.GroupChatParticipantLastRead.AlreadyExist);
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(GroupChatParticipantLastRead entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return user; }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return groupChat; }

            Result<GroupChatMessage> groupChatMessage = await GetFromDBAsync<GroupChatMessage>(gcm => gcm.Ordinal == entity.LastReadMessageId, PersistenceErrors.GroupChatMessage.NotFound, cancellationToken);
            if (groupChatMessage.IsFailure) { return groupChatMessage; }

            Result<GroupChatParticipantLastRead> groupChatParticipantLastRead = await GetFromDBAsync(
                gcplr =>
                    gcplr.LastReadMessageId == entity.LastReadMessageId &&
                    gcplr.GroupChatId == entity.GroupChatId &&
                    gcplr.UserId == entity.UserId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            if (groupChatParticipantLastRead.IsFailure) { return groupChatParticipantLastRead; }

            return Result.Success();
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(GroupChatParticipantLastRead entity, CancellationToken cancellationToken)
        {
            Result<GroupChatParticipantLastRead> groupChatParticipantLastRead = await GetFromDBAsync(
                gcplr =>
                    gcplr.LastReadMessageId == entity.LastReadMessageId &&
                    gcplr.GroupChatId == entity.GroupChatId &&
                    gcplr.UserId == entity.UserId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            if (groupChatParticipantLastRead.IsFailure) { return groupChatParticipantLastRead; }

            return Result.Success();
        }
    }
}
