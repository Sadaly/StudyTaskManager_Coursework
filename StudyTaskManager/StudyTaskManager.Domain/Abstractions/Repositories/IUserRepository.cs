using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="User">Ссылка на группу</param>
        void Add(Entity.User.User User);

        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> IsPhoneNumberUniqueAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
        Task<bool> IsUserNameUniqueAsync(UserName userName, CancellationToken cancellationToken = default);
    }
}
