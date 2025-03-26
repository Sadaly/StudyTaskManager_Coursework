using System;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Чат для текстового общения внутри группы
    /// </summary>
    public class GroupChat : BaseEntityWithID
    {
        // Приватный конструктор для EF Core
        private GroupChat()
        {
            _groupChatMessages = [];
            _groupChatParticipants = [];
        }
        // Приватный конструктор для инициализации объекта
        private GroupChat(Guid groupId, Title name, bool isPublic)
        {
            GroupId = groupId;
            Name = name;
            IsPublic = isPublic;
            _groupChatMessages = [];
            _groupChatParticipants = [];
        }

        #region свойства

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
        public Group? Group { get; private set; }

        /// <summary>
        /// Перечисление сообщений в чате
        /// </summary>
        public IEnumerable<GroupChatMessage> GroupChatMessages => _groupChatMessages;
        private List<GroupChatMessage> _groupChatMessages;

        /// <summary>
        /// Перечисление участников в чате
        /// </summary>
        public IEnumerable<GroupChatParticipant> GroupChatParticipants => _groupChatParticipants;
        private List<GroupChatParticipant> _groupChatParticipants;

        #endregion

        /// <summary>
        /// Фабричный метод для создания нового группового чата
        /// </summary>
        /// <param name="groupId">Идентификатор группы, к которой относится чат</param>
        /// <param name="name">Название чата</param>
        /// <param name="isPublic">Доступен ли чат всем участникам группы</param>
        /// <param name="group">Группа, к которой относится чат</param>
        /// <returns>Новый экземпляр группового чата</returns>
        public static Result<GroupChat> Create(Group group, Title name, bool isPublic)
        {
            GroupChat gc = new(group.Id, name, isPublic)
            {
                Group = group
            };

            gc.RaiseDomainEvent(new GroupChatCreatedDomainEvent(gc.Id));

            return Result.Success(gc);
        }

        /// <summary>
        /// Метод для добавления сообщения в чат
        /// </summary>
        /// <param name="message">Сообщение для добавления</param>
        public Result AddMessage(GroupChatMessage message)
        {
            _groupChatMessages?.Add(message);

            RaiseDomainEvent(new GroupChatAddedMessageDomainEvent(this.Id));

            return Result.Success();
        }

        /// <summary>
        /// Метод для добавления участника в чат
        /// </summary>
        /// <param name="participant">Участник для добавления</param>
        public Result AddParticipant(GroupChatParticipant participant)
        {
            _groupChatParticipants?.Add(participant);

            RaiseDomainEvent(new GroupChatAddedParticipantDomainEvent(this.Id));

            return Result.Success();
        }
    }
}
