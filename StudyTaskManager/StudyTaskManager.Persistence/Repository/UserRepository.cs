using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions.Repositories.Generic;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Repository;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Add(User user) => _dbContext.Set<User>().Add(user);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        !await _dbContext.Set<User>().AnyAsync(x => x.Email == email, cancellationToken);


    public async Task<bool> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
        !await _dbContext.Set<User>().AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

    public async Task<bool> IsUserNameUniqueAsync(UserName userName, CancellationToken cancellationToken = default) =>
        !await _dbContext.Set<User>().AnyAsync(x => x.UserName == userName, cancellationToken);

    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
