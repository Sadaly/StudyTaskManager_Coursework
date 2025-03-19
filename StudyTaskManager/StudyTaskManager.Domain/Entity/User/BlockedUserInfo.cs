using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Запись о том, что пользователь был заблокирован
    /// </summary>
    public class BlockedUserInfo : Common.BaseEntity
    {
        /// <summary>
        /// Конструктор класса <see cref="BlockedUserInfo"/>
        /// </summary>
        /// <param name="reason">Причина блокировки пользователя</param>
        /// <param name="prevRoleId">Идентификатор роли пользователя до блокировки</param>
        /// <param name="user">Ссылка на пользователя, который был заблокирован</param>
        private BlockedUserInfo(string reason, Guid prevRoleId, User user) : base()
        {
            Reason = reason;
            BlockedDate = DateTime.UtcNow; // Дата блокировки устанавливается автоматически
            PrevRoleId = prevRoleId;
            UserId = user.Id;
            User = user;
        }

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
        public User User { get; } = null!;

        /// <summary>
        /// Метод для создания новой записи о блокировке пользователя
        /// </summary>
        /// <param name="reason">Причина блокировки пользователя</param>
        /// <param name="prevRoleId">Идентификатор роли пользователя до блокировки</param>
        /// <param name="user">Ссылка на пользователя, который был заблокирован</param>
        /// <returns>Новый экземпляр класса <see cref="BlockedUserInfo"/></returns>
        public static BlockedUserInfo Create(string reason, Guid prevRoleId, User user)
        {
            var blockedUserInfo = new BlockedUserInfo(reason, prevRoleId, user);

            // Todo: Добавить создание события, связанного с блокировкой пользователя

            return blockedUserInfo;
        }
    }
}