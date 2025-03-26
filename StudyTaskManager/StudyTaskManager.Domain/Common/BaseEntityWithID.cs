using System.ComponentModel.DataAnnotations.Schema;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// Класс BaseEntityWithID содержит коллекцию событий предметной области, а также некоторые вспомогательные методы для добавления, удаления и очистки событий предметной области из этой коллекции.
    /// </summary>
    public abstract class BaseEntityWithID : BaseEntity
    {
        public Guid Id { get; protected set; }

        protected BaseEntityWithID() { }
        protected BaseEntityWithID(Guid id) { Id = id; }


        public override Result Delete()
        {
            DeleteFlag = true;

            RaiseDomainEvent(new EntityWithIdDeletedDomainEvent(this.Id, this.GetType().Name));

            return Result.Success();
        }
    }
}
