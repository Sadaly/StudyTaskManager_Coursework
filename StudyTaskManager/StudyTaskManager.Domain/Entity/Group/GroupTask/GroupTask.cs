using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Задача
    /// </summary>
    public class GroupTask : BaseEntityWithID
    {
        /// <summary>
        /// Id группы
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Id родительской задачи, если она есть
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Срок выполнения задачи
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Id статуса для задач
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// Заголовок задачи
        /// </summary>
        public GroupTaskHeadLine HeadLine { get; set; } = null!;

        /// <summary>
        /// Описание задачи
        /// </summary>
        public GroupTaskDescription? Description { get; set; }

        /// <summary>
        /// Id ответственного за задачу
        /// </summary>
        public Guid? ResponsibleId { get; set; }

        /// <summary>
        /// Индикатор, показывающий просрочен ли дедлайн
        /// </summary>
        private bool IsDeadLineExpired { get; set; }



        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; } = null!;

        /// <summary>
        /// Ссылка на родительскую задачу, если она есть
        /// </summary>
        public GroupTask? Parent { get; set; }

        /// <summary>
        /// Ссылка на статус
        /// </summary>
        public GroupTaskStatus Status { get; set; } = null!;

        /// <summary>
        /// Ответственный за задачу
        /// </summary>
        public User.User? ResponsibleUser { get; set; }
    }
}
