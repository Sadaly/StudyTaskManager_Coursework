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
        public string Reason { get; set; } = null!;

        /// <summary>
        /// Дата блокировки пользователя
        /// </summary>
        public DateTime BlockedDate { get; set; }



        /// <summary>
        /// Ссылка на самого пользователя
        /// </summary>
        public User User { get; set; } = null!;
    }
}
