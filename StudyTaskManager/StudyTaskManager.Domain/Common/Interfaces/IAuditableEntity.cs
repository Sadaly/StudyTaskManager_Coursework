namespace StudyTaskManager.Domain.Common.Interfaces
{
    /// <summary>
    /// Этот интерфейс добавляет дополнительные свойства для отслеживания информации о контрольном журнале объекта.
    /// </summary>
    public interface IAuditableEntity : BaseEntity
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
