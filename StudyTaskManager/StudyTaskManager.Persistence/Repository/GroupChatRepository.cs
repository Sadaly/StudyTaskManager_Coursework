using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupChatRepository : Generic.TWithIdRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupChat.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupChat.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupChat entity, CancellationToken cancellationToken)
        {
            Result<Group> group = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) { return group; }

            bool notUniqueName = await _dbContext.Set<GroupChat>().AnyAsync(gc => gc.Name.Value == entity.Name.Value, cancellationToken);
            if (notUniqueName) { return Result.Failure(PersistenceErrors.GroupChat.NotUniqueName); }

            Result<GroupChat> groupChat = await GetFromDBAsync(entity.Id, cancellationToken);
            if (groupChat.IsSuccess) { return Result.Failure(PersistenceErrors.GroupChat.AlreadyExist); }
            return Result.Success();
        }
    }
}
