using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Запись о том, что пользователь был заблокирован
    /// </summary>
    public class BlockedUserInfo : Common.BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Причина блокировки
        /// </summary>
        public BlockReason Reason { get; } = null!;

        /// <summary>
        /// Дата блокировки пользователя
        /// </summary>
        public DateTime BlockedDate { get; }

        /// <summary>
        /// Роль пользователя перед блокировкой
        /// </summary>
        public Guid PrevRoleId { get; }

        /// <summary>
        /// Ссылка на самого пользователя
        /// </summary>
        public User User { get; } = null!;
    }
}
