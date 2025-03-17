using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Действия для логов
    /// </summary>
    public class LogAction : BaseEntityWithID
    {
        /// <summary>
        /// Название действия
        /// </summary>
        public LogActionName Name { get; set; } = null!;

        /// <summary>
        /// Описание действия
        /// </summary>
        public LogActionDescription Description { get; set; } = null!;
    }
}
