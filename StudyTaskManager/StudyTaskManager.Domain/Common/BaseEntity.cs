using StudyTaskManager.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// Класс BaseEntity содержит коллекцию событий предметной области, а также некоторые вспомогательные методы для добавления, удаления и очистки событий предметной области из этой коллекции.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        private readonly List<BaseEvent> _domainEvents = [];

        public int Id { get; set; }

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
