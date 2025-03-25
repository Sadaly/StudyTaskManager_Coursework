using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IPresonalMessageRepository : Generic.IRepositoryWithID<PersonalMessage>
    {
        /// <summary>
        /// Возвращает все сообщения из этого чата.
        /// </summary>
        /// <param name="personalChat">Чат по которому ведется поиск.</param>
        Task<Result<List<PersonalMessage>>> GetMessageByChatAsync(PersonalChat personalChat, CancellationToken cancellationToken = default);
    }
}
