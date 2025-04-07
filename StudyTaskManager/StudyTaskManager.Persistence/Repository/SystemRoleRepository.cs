using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Repository
{
    class SystemRoleRepository : Generic.TWithIdRepository<SystemRole>, ISystemRoleRepository
    {
        public SystemRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(SystemRole systemRole, CancellationToken cancellationToken = default)
        {
            bool notUniqueTitle = await _dbContext.Set<SystemRole>().AnyAsync(sr => sr.Name.Value == systemRole.Name.Value, cancellationToken);
            if (notUniqueTitle) return Result.Failure(PersistenceErrors.SystemRole.NotUniqueName);

            await _dbContext.Set<SystemRole>().AddAsync(systemRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<SystemRole?>> GetByTitleAsync(Title title, CancellationToken cancellationToken = default)
        {
            SystemRole? systemRole = await _dbContext.Set<SystemRole>().FirstOrDefaultAsync(sr => sr.Name == title, cancellationToken);
            return Result.Success(systemRole);
        }
    }
}
