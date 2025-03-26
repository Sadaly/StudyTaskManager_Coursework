using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public interface IUserRepository : Generic.IRepositoryWithID<User>
    {
        Task<Result<bool>> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
        Task<Result<bool>> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
        Task<Result<bool>> IsUsernameUniqueAsync(Username username, CancellationToken cancellationToken = default);
    }
}
