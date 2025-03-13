namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище групп
    /// </summary>
    public interface IGroupChatRepository
    {
        /// <summary>
        /// Добавление нового группового чата
        /// </summary>
        /// <param name="GroupChat">Ссылка на групповой чат</param>
        void Add(Entity.Group.Chat.GroupChat GroupChat);

        /// <summary>
        /// Удаление группового чата
        /// </summary>
        /// <param name="GroupChat">Ссылка на групповой чат</param>
        void Remove(Entity.Group.Chat.GroupChat GroupChat);
    }
}
