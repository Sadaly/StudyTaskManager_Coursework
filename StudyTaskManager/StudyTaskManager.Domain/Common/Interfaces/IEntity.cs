namespace StudyTaskManager.Domain.Common.Interfaces
{
    /// <summary>
    /// Этот файл содержит базовый интерфейс IEntity, и все сущности предметной области будут реализовывать этот интерфейс прямо или косвенно.
    /// </summary>
    public interface IEntity
    {
        public Guid Id { get; }
    }
}
