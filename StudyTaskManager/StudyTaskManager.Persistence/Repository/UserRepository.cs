using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Repository
{
    public class UserRepository : Generic.TWithIdRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<bool>> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<Result<bool>> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        }

        public async Task<Result<bool>> IsUsernameUniqueAsync(Username username, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<Result<User>> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
        {
            return await GetFromDBAsync(u => u.Username == username, PersistenceErrors.User.NotFound, cancellationToken);
        }

        public async Task<Result<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await GetFromDBAsync(u => u.Email == email, PersistenceErrors.User.NotFound, cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.User.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.User.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(User entity, CancellationToken cancellationToken)
        {
            Result<bool> unique;

            unique = await IsEmailUniqueAsync(entity.Email, cancellationToken);
            if (unique.IsFailure) { return unique; }
            if (!unique.Value) { return Result.Failure(PersistenceErrors.User.NotUniqueEmail); }

            unique = await IsUsernameUniqueAsync(entity.Username, cancellationToken);
            if (unique.IsFailure) { return unique; }
            if (!unique.Value) { return Result.Failure(PersistenceErrors.User.NotUniqueUsername); }

            if (entity.PhoneNumber != null)
            {
                unique = await IsPhoneNumberUniqueAsync(entity.PhoneNumber, cancellationToken);
                if (unique.IsFailure) { return unique; }
                if (!unique.Value) { return Result.Failure(PersistenceErrors.User.NotUniquePhoneNumber); }
            }

            Result<object> obj;

            if (entity.SystemRoleId != null)
            {
                obj = await GetFromDBAsync<SystemRole>(
                    (Guid)entity.SystemRoleId,
                    PersistenceErrors.SystemRole.IdEmpty,
                    PersistenceErrors.SystemRole.NotFound,
                    cancellationToken);
                if (obj.IsFailure) { return obj; }
            }

            obj = GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.User.AlreadyExists);
        }
    }
}
