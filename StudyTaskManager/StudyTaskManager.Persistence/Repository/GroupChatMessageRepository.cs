using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupChatMessageRepository : Generic.TRepository<GroupChatMessage>, IGroupChatMessageRepository
    {
        public GroupChatMessageRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupChatMessage entity, CancellationToken cancellationToken)
        {
            Result<User> sender = await GetFromDBAsync<User>(entity.SenderId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (sender.IsFailure) { return sender; }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return groupChat; }

            if (!groupChat.Value.IsPublic)
            {
                Result<GroupChatParticipant> groupChatParticipant = await GetFromDBAsync<GroupChatParticipant>(
                    gcp =>
                        gcp.GroupChatId == entity.GroupChatId &&
                        gcp.UserId == entity.SenderId
                    , PersistenceErrors.GroupChatParticipant.NotFound
                    , cancellationToken);
                if (groupChatParticipant.IsFailure) { return Result.Failure(groupChatParticipant.Error); }
            }

            Result<GroupChatMessage> res = await GetFromDBAsync(
                gcm =>
                    gcm.SenderId == entity.SenderId &&
                    gcm.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            if (res.IsSuccess) { return Result.Failure(PersistenceErrors.GroupChatMessage.AlreadyExist); }
            return Result.Success();
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(GroupChatMessage entity, CancellationToken cancellationToken)
        {
            Result<User> sender = await GetFromDBAsync<User>(entity.SenderId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (sender.IsFailure) { return sender; }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return groupChat; }

            Result<GroupChatMessage> res = await GetFromDBAsync(
                gcm =>
                    gcm.SenderId == entity.SenderId &&
                    gcm.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            return res;
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(GroupChatMessage entity, CancellationToken cancellationToken)
        {
            Result<GroupChatMessage> res = await GetFromDBAsync(
                gcm =>
                    gcm.SenderId == entity.SenderId &&
                    gcm.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatMessage.NotFound
                , cancellationToken);
            return res;
        }
    }
}
