using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// Класс BaseEntityWithID содержит коллекцию событий предметной области, а также некоторые вспомогательные методы для добавления, удаления и очистки событий предметной области из этой коллекции.
    /// </summary>
    public abstract class BaseEntityWithID : BaseEntity
    {
        private readonly List<BaseEntityWithID> _domainEvents = [];

        [NotMapped]
        public IReadOnlyCollection<BaseEntityWithID> DomainEvents => _domainEvents.AsReadOnly();
        public Guid Id { get; protected set; }

        protected BaseEntityWithID() { }
        protected BaseEntityWithID(Guid id) { Id = id; }

        public void AddDomainEvent(BaseEntityWithID domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEntityWithID domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
