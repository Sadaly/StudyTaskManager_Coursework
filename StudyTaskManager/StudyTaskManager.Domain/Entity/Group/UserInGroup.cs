namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Пользователь в группе
    /// </summary>
    public class UserInGroup : Common.BaseEntity
    {
        /// <summary>
        /// Id группы
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Id роли 
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; }

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
        public User.User User { get; } = null!;
    }
}
