using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Repository
{
    class SystemRoleRepository : Generic.TWithIdRepository<SystemRole>, ISystemRoleRepository
    {
        public SystemRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<SystemRole?>> GetByTitleAsync(Title title, CancellationToken cancellationToken = default)
        {
            SystemRole? systemRole = await _dbContext.Set<SystemRole>().FirstOrDefaultAsync(sr => sr.Name == title, cancellationToken);
            return Result.Success(systemRole);
        }
    }
}
