namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Приглашение для пользователя в группу
    /// </summary>
    public class GroupInvite : Common.BaseEntity
    {
        private GroupInvite(User.User Sender, User.User Receiver, Group Group) : base()
        {
            this.Sender = Sender;
            this.SenderId = Sender.Id;

            this.Receiver = Receiver;
            this.ReceiverId = Receiver.Id;

            this.Group = Group;
            this.GroupId = Group.Id;

            DateInvitation = DateTime.UtcNow;
        }

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
        public Guid GroupId { get; }

        /// <summary>
        /// Флаг, показывающий было ли принято приглашение (null не принято. false отклонено. true принято)
        /// </summary>
        // Можно оставить только true/false и считать так: пока не принято false. Отклонено или принято true.
        // А можно вообще отказаться ведь если запись есть, то приглашение принято, если нет, то не принято
        public bool? InvitationAccepted { get; set; }


        /// <summary>
        /// Отправитель приглашения 
        /// </summary>
        public User.User Sender { get; } = null!;

        /// <summary>
        /// Получатель приглашения
        /// </summary>
        public User.User Receiver { get; } = null!;

        /// <summary>
        /// Группа в которую приглашают
        /// </summary>
        public Group Group { get; } = null!;
    }
}
