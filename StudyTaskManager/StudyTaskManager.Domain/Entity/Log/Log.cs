using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Лог действий в группах
    /// </summary>
    public class Log : BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Id группы
        /// </summary>
        public int? GroupId { get; }

        /// <summary>
        /// Id действия лога
        /// </summary>
        public int LogActionId { get; }

        /// <summary>
        /// Время действия
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Id человека, который является причиной появления запси в лог
        /// </summary>
        public int? InitiatorId { get; }

        /// <summary>
        /// Id человека, который был затрон действием, если он есть
        /// </summary>
        public int? SubjectId { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }



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
        public User.AbsUser? Initiator { get; }

        /// <summary>
        /// Ссылка на субъекта действия
        /// </summary>
        public User.AbsUser? Subject { get; }
    }
}
