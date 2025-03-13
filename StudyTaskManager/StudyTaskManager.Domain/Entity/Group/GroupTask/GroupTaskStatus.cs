using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Статус задачи
    /// </summary>
    public class GroupTaskStatus : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Id группы
        /// </summary>
        public int GroupId { get; }

        /// <summary>
        /// Может ли статус быть обновлен
        /// </summary>
        public bool Can_Be_Updated { get; set; }



        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group? Group { get; }
    }
}
