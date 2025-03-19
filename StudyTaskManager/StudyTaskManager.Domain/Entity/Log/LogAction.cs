using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Действие для логов, представляющее конкретное событие в системе.
    /// </summary>
    public class LogAction : BaseEntityWithID
    {
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="LogAction"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор действия.</param>
        /// <param name="name">Название действия.</param>
        /// <param name="description">Описание действия.</param>
        private LogAction(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Название действия.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Описание действия.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Метод для создания нового объекта <see cref="LogAction"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор действия.</param>
        /// <param name="name">Название действия.</param>
        /// <param name="description">Описание действия.</param>
        /// <returns>Новый экземпляр класса <see cref="LogAction"/>.</returns>
        public static LogAction Create(Guid id, string name, string description)
        {
            var logAction = new LogAction(id, name, description);

            logAction.RaiseDomainEvent(new LogActionCreatedDomainEvent(id));

            return logAction;
        }
    }
}
