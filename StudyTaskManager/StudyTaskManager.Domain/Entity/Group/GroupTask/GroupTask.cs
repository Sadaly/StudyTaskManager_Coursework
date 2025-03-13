using StudyTaskManager.Domain.Common;
using System.Diagnostics;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Задача
    /// </summary>
    public class GroupTask : BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Id группы
        /// </summary>
        public int GroupId { get; }

        /// <summary>
        /// Id родительской задачи, если она есть
        /// </summary>
        public int? ParentId { get; set; }

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
        public int StatusId { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Id ответственного за задачу
        /// </summary>
        public int? ResponsibleId { get; set; }



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
        public User.AbsUser? ResponsibleUser { get; set; }
    }
}
