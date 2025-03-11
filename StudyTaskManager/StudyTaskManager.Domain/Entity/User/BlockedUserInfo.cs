namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Запись о том, что пользователь был заблокирован
    /// </summary>
    public class BlockedUserInfo
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
        public AbsUser User { get; set; } = null!;
    }
}
