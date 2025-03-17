
using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// У класса BaseEvent есть только одно свойство DateHappened, которое сообщает нам, когда произошло конкретное событие.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
