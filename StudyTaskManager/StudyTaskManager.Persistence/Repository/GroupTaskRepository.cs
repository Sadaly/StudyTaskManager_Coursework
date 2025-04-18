using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupTaskRepository : Generic.TWithIdRepository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTask>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTask>()
                .Where(gt => gt.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupTask.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupTask.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupTask entity, CancellationToken cancellationToken)
        {
            if (entity.ParentId == entity.Id) return Result.Failure(PersistenceErrors.GroupTask.СannotParentForItself);

            var group = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) { return group; }

            var groupTaskStatus = await GetFromDBAsync<GroupTaskStatus>(entity.StatusId, PersistenceErrors.GroupTaskStatus.IdEmpty, PersistenceErrors.GroupTaskStatus.NotFound, cancellationToken);
            if (groupTaskStatus.IsFailure) { return groupTaskStatus; }

            // проверка, чтобы ответственный за задачу был обязан присутствовать в группе
            if (entity.ResponsibleUserId is not null)
            {
                var user = await GetFromDBAsync<User>(entity.ResponsibleUserId.Value, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
                if (user.IsFailure) { return user; }

                var userInGrup = await GetFromDBAsync<UserInGroup>(
                    uig =>
                        uig.GroupId == group.Value.Id &&
                        uig.UserId == user.Value.Id,
                    PersistenceErrors.UserInGroup.NotFound,
                    cancellationToken);
                if (userInGrup.IsFailure)
                {
                    if (userInGrup.Error == PersistenceErrors.UserInGroup.NotFound) { return Result.Failure(PersistenceErrors.GroupTask.UserIsNotInTheGroup); }
                    return userInGrup;
                }
            }

            var groupTask = await GetFromDBAsync(entity.Id, cancellationToken);
            if (groupTask.IsSuccess) { return Result.Failure(PersistenceErrors.GroupTaskStatus.AlreadyExists); }
            return Result.Success();
        }
    }
}
