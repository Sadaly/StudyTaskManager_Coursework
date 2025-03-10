using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Заблокированный пользователь
    /// </summary>
    public class BlockedUser: IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Ссылка на самого пользователя
        /// </summary>
        public User User { get; set; } = null!;
        /// <summary>
        /// Причина блокировки (not null)
        /// </summary>
        public string Reason { get; set; } = null!;
        /// <summary>
        /// Дата блокировки пользователя
        /// </summary>
        public DateTime Blocked_Date { get; set; }
    }
}
