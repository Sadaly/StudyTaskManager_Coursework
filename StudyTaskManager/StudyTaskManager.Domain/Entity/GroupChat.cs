using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Чат для текстового общения внутри группы
    /// </summary>
    public class GroupChat : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Ссылка на группу по id
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Название чата (not null)
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Модификатор, показывающий доступен ли чат всем участникам группы или нет
        /// </summary>
        public bool IsPublic { get; set; }


        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; set; } = null!;
        /// <summary>
        /// Перечисление сообщений в чате
        /// </summary>
        public IReadOnlyCollection<GroupChatMessage>? GroupChatMessages => _groupChatMessages;
        private List<GroupChatMessage>? _groupChatMessages;
        /// <summary>
        /// Перечисление участников в чате
        /// </summary>
        public IReadOnlyCollection<GroupChatParticipant>? GroupChatParticipants => _groupChatParticipants;
        private List<GroupChatParticipant>? _groupChatParticipants;
    }
}
