namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Приглашение для пользователь в группу
    /// </summary>
    public class GroupInvite
    {
        /// <summary>
        /// Id отправитель сообщения
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// Id получатель сообщения
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Отправитель сообщения
        /// </summary>
        public User Sender { get; set; } = null!;
        /// <summary>
        /// Получатель сообщения
        /// </summary>
        public User Receiver { get; set; } = null!;
    }
}
