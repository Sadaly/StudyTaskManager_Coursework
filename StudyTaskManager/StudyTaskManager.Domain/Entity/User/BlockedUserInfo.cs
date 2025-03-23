namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Запись о том, что пользователь был заблокирован
    /// </summary>
    public class BlockedUserInfo : Common.BaseEntity
    {
        // Приватный конструктор для EF Core
        private BlockedUserInfo() { }
        /// <summary>
        /// Конструктор класса <see cref="BlockedUserInfo"/>
        /// </summary>
        /// <param name="reason">Причина блокировки пользователя</param>
        /// <param name="prevRoleId">Идентификатор роли пользователя до блокировки</param>
        /// <param name="user">Ссылка на пользователя, который был заблокирован</param>
        private BlockedUserInfo(Guid userId, Guid prevRoleId, string reason)
        {
            UserId = userId;
            PrevRoleId = prevRoleId;
            Reason = reason;
            BlockedDate = DateTime.UtcNow; // Дата блокировки устанавливается автоматически
        }

        #region свойства

        public Guid UserId { get; }
        public string Reason { get; } = null!;
        public DateTime BlockedDate { get; }
        public Guid PrevRoleId { get; }
        public User? User { get; private set; } = null!;
        public SystemRole? PrevRole { get; }

        #endregion

        /// <summary>
        /// Метод для создания новой записи о блокировке пользователя
        /// </summary>
        /// <param name="reason">Причина блокировки пользователя</param>
        /// <param name="prevRoleId">Идентификатор роли пользователя до блокировки</param>
        /// <param name="user">Ссылка на пользователя, который был заблокирован</param>
        /// <returns>Новый экземпляр класса <see cref="BlockedUserInfo"/></returns>
        public static BlockedUserInfo Create(string reason, Guid prevRoleId, User user)
        {
            var blockedUserInfo = new BlockedUserInfo(user.Id, prevRoleId, reason)
            {
                User = user
            };

            // Todo: Добавить создание события, связанного с блокировкой пользователя

            return blockedUserInfo;
        }
    }
}