using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    public class UserRepository : Generic.TWithIdRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
        }

        public async Task<bool> IsUserNameUniqueAsync(UserName userName, CancellationToken cancellationToken = default)
        {
            return !await _dbContext.Set<User>().AnyAsync(x => x.UserName == userName, cancellationToken);
        }
    }
}
