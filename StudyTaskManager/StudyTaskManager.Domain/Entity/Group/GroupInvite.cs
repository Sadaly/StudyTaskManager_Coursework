namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Приглашение для пользователя в группу
    /// </summary>
    public class GroupInvite
    {
        /// <summary>
        /// Id отправителя приглашения
        /// </summary>
        public int SenderId { get; }

        /// <summary>
        /// Id получателя приглашеия
        /// </summary>
        public int ReceiverId { get; }

        /// <summary>
        /// Дата отправки приглашения
        /// </summary>
        public DateTime DateInvitation { get; }

        /// <summary>
        /// ID группы в которую приглашают
        /// </summary>
        public int IDGroup { get; }

        /// <summary>
        /// Флаг, показывающий было ли принято приглашение
        /// </summary>
        // Наличие этого свойства под вопросом,
        // по умолчанию каждое приглашение еще не принято.
        // Как только пользователь приглашение принял,
        // запись в таблице должна быть удалена и добавлена новая запись в таблицу "пользователь в группе".
        public bool InvitationAccepted { get; set; }


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
