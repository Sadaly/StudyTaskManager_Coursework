namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Пользователь в группе
    /// </summary>
    public class UserInGroup
    {
        /// <summary>
        /// Id группы
        /// </summary>
        public int GroupId { get; }

        /// <summary>
        /// Id роли 
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Время вступления
        /// </summary>
        public DateTime DateEntered { get; }



        /// <summary>
        /// Ссылка на группа
        /// </summary>
        public Group Group { get; } = null!;

        /// <summary>
        /// Ссылка на роль
        /// </summary>
        public GroupRole Role { get; set; } = null!;

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public User User { get; } = null!;
    }
}
