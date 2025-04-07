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
            return !await _dbContext.Set<User>().AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<Result<bool>> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
        }

        public async Task<Result<bool>> IsUsernameUniqueAsync(Username username, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(x => x.Username == username, cancellationToken);
        }

        public async Task<Result<User?>> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
            return Result.Success(user);
        }

        public async Task<Result<User?>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            return Result.Success(user);
        }

        public override async Task<Result> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            bool notUniqueUsername = await _dbContext.Set<User>().AnyAsync(u => u.Username.Value == user.Username.Value, cancellationToken);
            if (notUniqueUsername) return Result.Failure(PersistenceErrors.User.NotUniqueUsername);

            bool notUniqueEmail = await _dbContext.Set<User>().AnyAsync(u => u.Email.Value == user.Email.Value, cancellationToken);
            if (notUniqueEmail) return Result.Failure(PersistenceErrors.User.NotUniqueEmail);

            if (user.SystemRoleId != null)
            {
                SystemRole? systemRole = await _dbContext.Set<SystemRole>().FirstOrDefaultAsync(sr => sr.Id == user.SystemRoleId, cancellationToken);
                if (systemRole == null) return Result.Failure(PersistenceErrors.SystemRole.NotFound);
            }

            await _dbContext.Set<User>().AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
