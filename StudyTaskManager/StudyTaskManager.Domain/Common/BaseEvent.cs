// Этот файл содержит класс BaseEvent, который станет базовым классом для всех событий предметной области в приложении.

using MediatR;

namespace StudyTaskManager.Domain.Common
{
    /// <summary>
    /// У класса BaseEvent есть только одно свойство DateHappened, которое сообщает нам, когда произошло конкретное событие.
    /// </summary>
    public abstract class BaseEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
