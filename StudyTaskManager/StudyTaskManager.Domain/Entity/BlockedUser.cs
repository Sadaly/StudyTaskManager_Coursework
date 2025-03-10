namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Заблокированный пользователь
    /// </summary>
    public class BlockedUser
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int UserId { get; }

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
