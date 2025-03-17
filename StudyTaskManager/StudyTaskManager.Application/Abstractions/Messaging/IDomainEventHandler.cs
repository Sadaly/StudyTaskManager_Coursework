using MediatR;
using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Application.Abstractions.Messaging
{
    /// <summary>
    /// Обработчик Domain событий
    /// </summary>
    /// <typeparam name="TEvent">Обрабатываемое событие</typeparam>
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
