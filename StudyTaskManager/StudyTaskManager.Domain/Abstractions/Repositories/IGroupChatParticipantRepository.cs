using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище участников групп
    /// </summary>
    public interface IGroupChatParticipantRepository
    {
        /// <summary>
        /// Добавление нового участника группы
        /// </summary>
        /// <param name="GroupChatParticipant">Ссылка на групповой чат</param>
        void Add(GroupChatParticipant GroupChatParticipant);

        /// <summary>
        /// Удаление участника группы
        /// </summary>
        /// <param name="GroupChatParticipant">Ссылка на групповой чат</param>
        void Remove(GroupChatParticipant GroupChatParticipant);
    }
}
