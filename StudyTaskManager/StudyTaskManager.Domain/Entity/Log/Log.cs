using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Лог действий в группах
    /// </summary>
    public class Log : BaseEntityWithID
    {
        /// <summary>
        /// Id группы
        /// </summary>
        public Guid? GroupId { get; }

        /// <summary>
        /// Id действия лога
        /// </summary>
        public Guid LogActionId { get; }

        /// <summary>
        /// Время действия
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Id человека, который является причиной появления запси в лог
        /// </summary>
        public Guid? InitiatorId { get; }

        /// <summary>
        /// Id человека, который был затрон действием, если он есть
        /// </summary>
        public Guid? SubjectId { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public LogDescription? Description { get; set; }



        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group.Group? Group { get; }

        /// <summary>
        /// Ссылка на лог действие
        /// </summary>
        public LogAction LogAction { get; } = null!;

        /// <summary>
        /// Ссылка на инициатора действия
        /// </summary>
        public User.User? Initiator { get; }

        /// <summary>
        /// Ссылка на субъекта действия
        /// </summary>
        public User.User? Subject { get; }
    }
}
