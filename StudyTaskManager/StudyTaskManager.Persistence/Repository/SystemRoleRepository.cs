using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Repository
{
    public class SystemRoleRepository : Generic.TWithIdRepository<SystemRole>, ISystemRoleRepository
    {
        public SystemRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<SystemRole>> GetByTitleAsync(Title title, CancellationToken cancellationToken = default)
        {
            return await GetFromDBAsync(sr => sr.Name == title, cancellationToken);
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
            bool notUniqueTitle = await _dbContext.Set<SystemRole>().AnyAsync(sr => sr.Name == entity.Name, cancellationToken);
            if (notUniqueTitle) { return Result.Failure(PersistenceErrors.SystemRole.NotUniqueName); }

            Result<SystemRole> systemRole = await GetFromDBAsync(entity.Id, cancellationToken);
            if (systemRole.IsSuccess) { return Result.Failure(PersistenceErrors.SystemRole.AlreadyExists); }
            return Result.Success();
        }
    }
}
