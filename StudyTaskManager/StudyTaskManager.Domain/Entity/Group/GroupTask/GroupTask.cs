using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Задача, относящаяся к группе для выполнения и выполнения задач.
    /// </summary>
    public class GroupTask : BaseEntityWithID
    {
        private GroupTask(Guid id, Guid GroupId, DateTime deadline, Guid StatusId) : base(id)
        {
            this.GroupId = GroupId;
            this.StatusId = StatusId;

            DateCreated = DateTime.UtcNow;
            Deadline = deadline;
        }
        private GroupTask(Guid id, Guid GroupId, DateTime deadline, Guid StatusId, Title headLine, Guid? ResponsibleUserId, Guid? ParentTaskId)
            : this(id, GroupId, deadline, StatusId)
        {
            this.HeadLine = headLine;
            this.ResponsibleId = ResponsibleUserId;
            this.ParentId = ParentTaskId;
        }

        #region свойства

        /// <summary>
        /// Id группы, к которой относится задача.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Id родительской задачи, если она есть.
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Срок выполнения задачи.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Id статуса для задачи.
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Заголовок задачи.
        /// </summary>
        public Title HeadLine { get; set; } = null!;

        /// <summary>
        /// Описание задачи.
        /// </summary>
        public Content? Description { get; set; }

        /// <summary>
        /// Id ответственного за задачу.
        /// </summary>
        public Guid? ResponsibleId { get; set; }

        /// <summary>
        /// Индикатор, показывающий, просрочен ли дедлайн.
        /// </summary>
        public bool IsDeadLineExpired => DateTime.UtcNow > Deadline;

        /// <summary>
        /// Ссылка на группу, к которой относится задача.
        /// </summary>
        public Group? Group { get; private set; } = null!;

        /// <summary>
        /// Ссылка на родительскую задачу, если она есть.
        /// </summary>
        public GroupTask? Parent { get; private set; }

        /// <summary>
        /// Ссылка на статус задачи.
        /// </summary>
        public GroupTaskStatus? Status { get; set; } = null!;

        /// <summary>
        /// Id ответственного за задачу (может не быть).
        /// </summary>
        public Guid? ResponsibleUserId { get; set; }
        /// <summary>
        /// Ответственный за задачу (может не быть).
        /// </summary>
        public User.User? ResponsibleUser { get; set; }

        #endregion

        /// <summary>
        /// Метод для создания новой задачи.
        /// </summary>
        /// <param name="Id">Уникальный идентификатор задачи.</param>
        /// <param name="Group">Группы, к которой относится задача.</param>
        /// <param name="Deadline">Срок выполнения задачи.</param>
        /// <param name="Status">Статус задачи.</param>
        /// <param name="HeadLine">Заголовок задачи.</param>
        /// <param name="Description">Описание задачи.</param>
        /// <param name="ResponsibleUser">Ответственный за задачу.</param>
        /// <param name="Parent">Родительская задача, если она есть.</param>
        /// <returns>Новая задача.</returns>
        public static GroupTask Create(Guid Id, Group Group, DateTime Deadline, GroupTaskStatus Status, Title HeadLine, Content? Description, User.User? ResponsibleUser, GroupTask? Parent)
        {
            return new GroupTask(Id, Group.Id, Deadline, Status.Id, HeadLine, ResponsibleUser?.Id, Parent?.Id)
            {
                Group = Group,
                Status = Status,
                Description = Description,
                ResponsibleUser = ResponsibleUser,
                Parent = Parent
            };
        }
    }
}
