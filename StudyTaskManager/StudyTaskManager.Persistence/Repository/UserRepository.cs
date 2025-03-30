using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
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

		public async Task<Result<User>> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
		{
			return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
		}

        public async Task<Result<User>> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
    }
}
