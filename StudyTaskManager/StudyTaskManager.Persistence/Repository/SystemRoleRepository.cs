using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Persistence.Repository
{
    class SystemRoleRepository : Generic.TWithIdRepository<SystemRole>, ISystemRoleRepository
    {
        public SystemRoleRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
