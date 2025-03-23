using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Запись лога действий в группах.
    /// </summary>
    public class Log : BaseEntityWithID
    {
        private Log(Guid id, Guid logActionId) : base(id)
        {
            LogActionId = logActionId;
            DateTime = DateTime.UtcNow;
        }
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="Log"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор лога.</param>
        /// <param name="logAction">Действие, которое произошло.</param>
        /// <param name="description">Описание действия (опционально).</param>
        /// <param name="group">Группа, в которой произошло действие (если применимо).</param>
        /// <param name="initiator">Пользователь, инициировавший действие (если применимо).</param>
        /// <param name="subject">Пользователь, на которого повлияло действие (если применимо).</param>
        private Log(Guid id, Guid logActionId, string? description, Group.Group? group, User.User? initiator, User.User? subject)
            : this(id, logActionId)
        {
            Description = description;

            if (group != null)
            {
                GroupId = group.Id;
                Group = group;
            }

            if (initiator != null)
            {
                InitiatorId = initiator.Id;
                Initiator = initiator;
            }

            if (subject != null)
            {
                SubjectId = subject.Id;
                Subject = subject;
            }
        }

        #region свойства

        public Guid LogActionId { get; }
        public LogAction? LogAction { get; private set; }

        public string? Description { get; private set; }
        public DateTime DateTime { get; }

        public Guid? GroupId { get; }
        public Group.Group? Group { get; }

        public Guid? InitiatorId { get; }
        public User.User? Initiator { get; }

        public Guid? SubjectId { get; }
        public User.User? Subject { get; }

        #endregion

        /// <summary>
        /// Создает новый объект <see cref="Log"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор лога.</param>
        /// <param name="logAction">Действие, которое произошло.</param>
        /// <param name="description">Описание действия (опционально).</param>
        /// <param name="group">Группа, в которой произошло действие (если применимо).</param>
        /// <param name="initiator">Пользователь, инициировавший действие (если применимо).</param>
        /// <param name="subject">Пользователь, на которого повлияло действие (если применимо).</param>
        /// <returns>Новый экземпляр класса <see cref="Log"/>.</returns>
        public static Log Create(Guid id, LogAction logAction, string? description, Group.Group? group, User.User? initiator, User.User? subject)
        {
            var log = new Log(id, logAction.Id, description, group, initiator, subject)
            {
                LogAction = logAction
            };

            log.RaiseDomainEvent(new LogCreatedDomainEvent(id));

            return log;
        }
    }
}
