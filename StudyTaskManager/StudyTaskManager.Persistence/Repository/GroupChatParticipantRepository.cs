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

        public override async Task<Result> AddAsync(GroupChatParticipant groupChatParticipant, CancellationToken cancellationToken = default)
        {
            throw new Exception();
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) return Result.Failure(user.Error);

            Result<GroupChat> groupChat = await GetFromDBAsync<GroupChat>(entity.GroupChatId, PersistenceErrors.GroupChat.IdEmpty, PersistenceErrors.GroupChat.NotFound, cancellationToken);
            if (groupChat.IsFailure) return Result.Failure(groupChat.Error);
            if (groupChat.Value.IsPublic) { return Result.Failure(PersistenceErrors.GroupChatParticipant.AddingToAPublicChat); }

            Error notFound = PersistenceErrors.GroupChatParticipant.NotFound;
            Result<GroupChatParticipant> groupChatParticipant = await GetFromDBAsync(
                gcp => gcp.UserId == entity.UserId && gcp.GroupChatId == entity.GroupChatId
                , notFound
                , cancellationToken);
            if (groupChatParticipant.IsFailure)
            {
                if (groupChatParticipant.Error == notFound) return Result.Success();
                return groupChatParticipant;
            }
            return Result.Failure(PersistenceErrors.GroupChatParticipant.AlreadyExist);
        }

        protected override Task<Result> VerificationBeforeUpdateAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<Result> VerificationBeforeRemoveAsync(GroupChatParticipant entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
