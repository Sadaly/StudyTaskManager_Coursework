using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Действия для логов
    /// </summary>
    public class LogAction : BaseEntity
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
