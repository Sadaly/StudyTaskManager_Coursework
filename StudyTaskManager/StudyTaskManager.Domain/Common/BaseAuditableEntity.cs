using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// Этот класс является дочерним классом BaseEntity и реализует интерфейс IAuditableEntity, определенный выше.
    /// </summary>
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
