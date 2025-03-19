using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Чат для текстового общения внутри группы
    /// </summary>
    public class GroupChat : BaseEntityWithID
	{
        /// <summary>
        /// Ссылка на группу по id
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Название чата (not null)
        /// </summary>
        public Title Name { get; set; } = null!;

        /// <summary>
        /// Модификатор, показывающий доступен ли чат всем участникам группы или нет
        /// </summary>
        public bool IsPublic { get; set; }


        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; } = null!;

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
