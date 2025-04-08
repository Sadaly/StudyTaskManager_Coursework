using StudyTaskManager.Domain.Shared;

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
        private BlockedUserInfo(Guid userId, Guid prevRoleId, string reason) : base()
        {
            UserId = userId;
            PrevRoleId = prevRoleId;
            Reason = reason;
            BlockedDate = DateTime.UtcNow; // Дата блокировки устанавливается автоматически
        }

        #region свойства

        /// <summary>
        /// Уникальный идентификатор пользователя, который был заблокирован
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Причина блокировки пользователя
        /// </summary>
        public string Reason { get; } = null!;

        /// <summary>
        /// Дата и время блокировки пользователя
        /// </summary>
        public DateTime BlockedDate { get; }

        /// <summary>
        /// Идентификатор роли пользователя до блокировки
        /// </summary>
        public Guid PrevRoleId { get; }

        /// <summary>
        /// Ссылка на пользователя, который был заблокирован
        /// </summary>
        public User? User { get; private set; } = null!;

        /// <summary>
        /// Ссылка на роль пользователя до блокировки
        /// </summary>
        public SystemRole? PrevRole { get; }

        #endregion

        /// <summary>
        /// Метод для создания новой записи о блокировке пользователя
        /// </summary>
        /// <param name="reason">Причина блокировки пользователя</param>
        /// <param name="prevRoleId">Идентификатор роли пользователя до блокировки</param>
        /// <param name="user">Ссылка на пользователя, который был заблокирован</param>
        /// <returns>Новый экземпляр класса <see cref="BlockedUserInfo"/></returns>
        public static Result<BlockedUserInfo> Create(string reason, Guid prevRoleId, User user)
        {
            var blockedUserInfo = new BlockedUserInfo(user.Id, prevRoleId, reason)
            {
                User = user
            };

            blockedUserInfo.RaiseDomainEvent(new DomainEvents.BlockedUserDomainEvent(blockedUserInfo.UserId));

            return Result.Success(blockedUserInfo);
        }
    }
}