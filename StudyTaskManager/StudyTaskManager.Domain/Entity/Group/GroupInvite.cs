namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Приглашение для пользователя в группу
    /// </summary>
    public class GroupInvite : Common.BaseEntity
    {
        /// <summary>
        /// Id отправителя приглашения
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// Id получателя приглашения
        /// </summary>
        public Guid ReceiverId { get; }

        /// <summary>
        /// Дата отправки приглашения
        /// </summary>
        public DateTime DateInvitation { get; }

        /// <summary>
        /// ID группы в которую приглашают
        /// </summary>
        public Guid IDGroup { get; }

        /// <summary>
        /// Флаг, показывающий было ли принято приглашение (null не принято. false отклонено. true принято)
        /// </summary>
        // Можно оставить только true/false и считать так: пока не принято false. Отклонено или принято true.
        // А можно вообще отказаться ведь если запись есть, то приглашение принято, если нет, то не принято
        public bool? InvitationAccepted { get; set; }


        /// <summary>
        /// Отправитель приглашения 
        /// </summary>
        public User.AbsUser Sender { get; } = null!;

        /// <summary>
        /// Получатель приглашения
        /// </summary>
        public User.AbsUser Receiver { get; } = null!;

        /// <summary>
        /// Группа в которую приглашают
        /// </summary>
        public Group Group { get; } = null!;
    }
}
