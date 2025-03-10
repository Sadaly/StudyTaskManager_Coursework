using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Чат для текстового общения внутри группы
    /// </summary>
    public class GroupChat : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; set; } = null!;
        /// <summary>
        /// Название чата (not null)
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Модификатор, показывающий доступен ли чат всем участникам группы или нет
        /// </summary>
        public bool Is_Public { get; set; }
    }
}
