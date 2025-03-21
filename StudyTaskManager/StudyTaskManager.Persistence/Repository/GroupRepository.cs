using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRepository : Generic.TWithIdRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
