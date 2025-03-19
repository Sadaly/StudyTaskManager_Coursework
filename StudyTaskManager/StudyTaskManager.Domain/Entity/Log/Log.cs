using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Запись лога действий в группах.
    /// </summary>
    public class Log : BaseEntityWithID
    {
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="Log"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор лога.</param>
        /// <param name="logAction">Действие, которое произошло.</param>
        /// <param name="description">Описание действия (опционально).</param>
        /// <param name="group">Группа, в которой произошло действие (если применимо).</param>
        /// <param name="initiator">Пользователь, инициировавший действие (если применимо).</param>
        /// <param name="subject">Пользователь, на которого повлияло действие (если применимо).</param>
        private Log(Guid id, LogAction logAction, string? description, Group.Group? group, User.User? initiator, User.User? subject)
            : base(id)
        {
            LogAction = logAction;
            Description = description;
            DateTime = DateTime.UtcNow;

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

        /// <summary>
        /// Идентификатор группы, в которой произошло действие (если применимо).
        /// </summary>
        public Guid? GroupId { get; }

        /// <summary>
        /// Идентификатор действия лога.
        /// </summary>
        public Guid LogActionId => LogAction.Id;

        /// <summary>
        /// Время фиксации действия.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Идентификатор пользователя, который инициировал действие (если применимо).
        /// </summary>
        public Guid? InitiatorId { get; }

        /// <summary>
        /// Идентификатор пользователя, на которого повлияло действие (если применимо).
        /// </summary>
        public Guid? SubjectId { get; }

        /// <summary>
        /// Описание действия.
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Ссылка на группу, в которой произошло действие.
        /// </summary>
        public Group.Group? Group { get; }

        /// <summary>
        /// Ссылка на объект действия лога.
        /// </summary>
        public LogAction LogAction { get; }

        /// <summary>
        /// Ссылка на пользователя, инициировавшего действие.
        /// </summary>
        public User.User? Initiator { get; }

        /// <summary>
        /// Ссылка на пользователя, на которого повлияло действие.
        /// </summary>
        public User.User? Subject { get; }

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
            var log = new Log(id, logAction, description, group, initiator, subject);

            // TODO: Добавить создание события, связанного с логированием действия.

            return log;
        }
    }
}
