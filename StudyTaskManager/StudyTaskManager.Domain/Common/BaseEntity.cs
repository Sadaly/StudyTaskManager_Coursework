using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// Класс BaseEntity содержит коллекцию событий предметной области, а также некоторые вспомогательные методы для добавления, удаления и очистки событий предметной области из этой коллекции.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        public bool DeleteFlag { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
            _domainEvents.Add(domainEvent);

        public virtual Result Delete()
        {
            DeleteFlag = true;

            RaiseDomainEvent(new EntityDeletedDomainEvent(this, this.GetType().Name));

            return Result.Success();
        }
    }
}
