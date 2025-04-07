using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRepository : Generic.TWithIdRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(Group group, CancellationToken cancellationToken = default)
        {
            // проверка на уникальность названия группы наврятли пригодится
            // bool notUniqueTitle = await _dbContext.Set<Group>().AnyAsync(g => g.Title.Value == group.Title.Value, cancellationToken);

            GroupRole? groupRole = await _dbContext.Set<GroupRole>().FirstOrDefaultAsync(gr => gr.Id == group.DefaultRoleId, cancellationToken);
            if (groupRole == null) return Result.Failure(PersistenceErrors.GroupRole.NotFound);

            await _dbContext.Set<Group>().AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
