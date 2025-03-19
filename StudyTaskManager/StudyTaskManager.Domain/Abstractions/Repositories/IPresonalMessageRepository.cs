using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    interface IPresonalMessageRepository : Generic.IRepositoryWithID<PersonalMessage>
    {
        /// <summary>
        /// Возвращает все сообщения из этого чата.
        /// </summary>
        /// <param name="personalChat">Чат по которому ведется поиск.</param>
        Task<List<PersonalMessage>> GetMessageByChatAsync(PersonalChat personalChat);
    }
}
