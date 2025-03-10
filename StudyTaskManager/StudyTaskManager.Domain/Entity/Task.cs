using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Task : IEntity
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
        /// Id родительской задачи
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
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; } = null!;

        /// <summary>
        /// Ссылка на родительскую задачу
        /// </summary>
        public Task? Parent { get; set; }

        /// <summary>
        /// Ссылка на статус
        /// </summary>
        public TaskStatus Status { get; set; } = null!;
    }
}
