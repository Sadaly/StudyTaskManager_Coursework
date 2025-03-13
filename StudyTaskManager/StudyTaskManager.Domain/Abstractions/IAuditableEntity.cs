namespace StudyTaskManager.Domain.Abstractions;

/// <summary>
/// Этот интерфейс добавляет дополнительные свойства для отслеживания информации о контрольном журнале объекта.
/// </summary>
public interface IAuditableEntity : IEntity
{
    int? CreatedBy { get; set; }
    DateTime? CreatedDate { get; set; }
    int? UpdatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
}
