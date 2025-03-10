using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Действия для логов
    /// </summary>
    public class LogAction : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название действия
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание действия
        /// </summary>
        public string Description { get; set; } = null!;
    }
}
