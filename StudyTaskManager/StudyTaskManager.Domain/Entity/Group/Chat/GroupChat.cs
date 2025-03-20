using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Чат для текстового общения внутри группы
    /// </summary>
    public class GroupChat : BaseEntityWithID
    {
        // Приватный конструктор для инициализации объекта
        private GroupChat(Guid groupId, Title name, bool isPublic, Group group)
        {
            GroupId = groupId;
            Name = name;
            IsPublic = isPublic;
            Group = group;
            _groupChatMessages = new List<GroupChatMessage>();
            _groupChatParticipants = new List<GroupChatParticipant>();
        }

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
        public Group Group { get; }

        /// <summary>
        /// Перечисление сообщений в чате
        /// </summary>
        public IReadOnlyCollection<GroupChatMessage> GroupChatMessages => _groupChatMessages;
        private List<GroupChatMessage> _groupChatMessages;

        /// <summary>
        /// Перечисление участников в чате
        /// </summary>
        public IReadOnlyCollection<GroupChatParticipant> GroupChatParticipants => _groupChatParticipants;
        private List<GroupChatParticipant> _groupChatParticipants;

        /// <summary>
        /// Фабричный метод для создания нового группового чата
        /// </summary>
        /// <param name="groupId">Идентификатор группы, к которой относится чат</param>
        /// <param name="name">Название чата</param>
        /// <param name="isPublic">Доступен ли чат всем участникам группы</param>
        /// <param name="group">Группа, к которой относится чат</param>
        /// <returns>Новый экземпляр группового чата</returns>
        public static GroupChat Create(Guid groupId, Title name, bool isPublic, Group group)
		{
			//Todo добавить событие
			return new GroupChat(groupId, name, isPublic, group);
        }

        /// <summary>
        /// Метод для добавления сообщения в чат
        /// </summary>
        /// <param name="message">Сообщение для добавления</param>
        public void AddMessage(GroupChatMessage message)
		{
			//Todo добавить событие
			_groupChatMessages?.Add(message);
        }

        /// <summary>
        /// Метод для добавления участника в чат
        /// </summary>
        /// <param name="participant">Участник для добавления</param>
        public void AddParticipant(GroupChatParticipant participant)
		{
			//Todo добавить событие
			_groupChatParticipants?.Add(participant);
        }
    }
}
