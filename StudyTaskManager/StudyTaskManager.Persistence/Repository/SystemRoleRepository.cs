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

        public async Task<Result<SystemRole?>> GetByTitleAsync(Title title, CancellationToken cancellationToken = default)
        {
            SystemRole? systemRole = await _dbContext.Set<SystemRole>().FirstOrDefaultAsync(sr => sr.Name == title, cancellationToken);
            return Result.Success(systemRole);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.SystemRole.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.SystemRole.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(SystemRole entity, CancellationToken cancellationToken)
        {
            bool notUniqueTitle = await _dbContext.Set<SystemRole>().AnyAsync(sr => sr.Name.Value == entity.Name.Value, cancellationToken);
            if (notUniqueTitle) { return Result.Failure(PersistenceErrors.SystemRole.NotUniqueName); }

            Result<SystemRole> systemRole = await GetFromDBAsync(entity.Id, cancellationToken);
            if (systemRole.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.SystemRole.AlreadyExists);
        }
    }
}
