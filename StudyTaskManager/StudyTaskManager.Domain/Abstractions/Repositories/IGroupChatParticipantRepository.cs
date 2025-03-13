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
        void Add(Entity.Group.Chat.GroupChatParticipant GroupChatParticipant);

        /// <summary>
        /// Удаление участника группы
        /// </summary>
        /// <param name="GroupChatParticipant">Ссылка на групповой чат</param>
        void Remove(Entity.Group.Chat.GroupChatParticipant GroupChatParticipant);
    }
}
