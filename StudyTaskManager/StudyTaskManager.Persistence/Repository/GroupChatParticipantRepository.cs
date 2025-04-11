using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupChatParticipantRepository : Generic.TRepository<GroupChatParticipant>, IGroupChatParticipantRepository
    {
        public GroupChatParticipantRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return Result.Failure(user.Error); }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return Result.Failure(groupChat.Error); }
            if (groupChat.Value.IsPublic) { return Result.Failure(PersistenceErrors.GroupChatParticipant.AddingToAPublicChat); }
                     
            Result<GroupChatParticipant> groupChatParticipant = await GetFromDBAsync(
                gcp => 
                    gcp.UserId == entity.UserId && 
                    gcp.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatParticipant.NotFound
                , cancellationToken);
            if (groupChatParticipant.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.GroupChatParticipant.AlreadyExist);
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return Result.Failure(user.Error); }

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) { return Result.Failure(groupChat.Error); }
            if (groupChat.Value.IsPublic) { return Result.Failure(PersistenceErrors.GroupChatParticipant.AddingToAPublicChat); }

            Result<GroupChatParticipant> groupChatParticipant = await GetFromDBAsync(
                gcp => gcp.UserId == entity.UserId && gcp.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatParticipant.NotFound
                , cancellationToken);
            return groupChatParticipant;
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            Result<GroupChatParticipant> groupChatParticipant = await GetFromDBAsync(
                gcp => gcp.UserId == entity.UserId && gcp.GroupChatId == entity.GroupChatId
                , PersistenceErrors.GroupChatParticipant.NotFound
                , cancellationToken);
            return groupChatParticipant;
        }
    }
}
