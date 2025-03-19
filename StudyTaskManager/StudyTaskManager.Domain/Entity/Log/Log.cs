using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Лог действий в группах
    /// </summary>
    public class Log : BaseEntityWithID
    {
        private Log(Guid id, LogAction LogAction, string? Description, Group.Group? Group, User.User? Initiator, User.User? Subject) : base(id)
        {
            if (Group != null)
            {
                this.GroupId = Group.Id;
                this.Group = Group;
            }
            if (Initiator != null)
            {
                this.InitiatorId = Initiator.Id;
                this.Initiator = Initiator;
            }
            if (Subject != null)
            {
                this.SubjectId = Subject.Id;
                this.Subject = Subject;
            }
            this.Description = Description;
            this.LogAction = LogAction;
            this.DateTime = DateTime.UtcNow;
        }

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
        public User.User? Initiator { get; }

        /// <summary>
        /// Ссылка на субъекта действия
        /// </summary>
        public User.User? Subject { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LogAction"></param>
        /// <param name="Description"></param>
        /// <param name="Group"></param>
        /// <param name="Initiator"></param>
        /// <param name="Subject"></param>
        /// <returns></returns>
        public Log Create(Guid id, LogAction LogAction, string? Description, Group.Group? Group, User.User? Initiator, User.User? Subject)
        {
            var Log = new Log(id, LogAction, Description, Group, Initiator, Subject);

            //Todo создание события

            return Log;
        }
    }
}
