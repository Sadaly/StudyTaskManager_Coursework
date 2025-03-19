using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Запись о том, что пользователь был заблокирован
    /// </summary>
    public class BlockedUserInfo : Common.BaseEntity
    {
        private BlockedUserInfo(string Reason, Guid PrevRoleId, User User) : base()
        {
            this.Reason = Reason;
            this.BlockedDate = DateTime.UtcNow;
            this.PrevRoleId = PrevRoleId;
            this.UserId = User.Id;
            this.User = User;
        }

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Причина блокировки
        /// </summary>
        public string Reason { get; } = null!;

        /// <summary>
        /// Дата блокировки пользователя
        /// </summary>
        public DateTime BlockedDate { get; }

        /// <summary>
        /// Роль пользователя перед блокировкой
        /// </summary>
        public Guid PrevRoleId { get; }

        /// <summary>
        /// Ссылка на самого пользователя
        /// </summary>
        public User User { get; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reason"></param>
        /// <param name="BlockedDate"></param>
        /// <param name="PrevRoleId"></param>
        /// <param name="User"></param>
        /// <returns>Новый экземпляр класс <see cref="BlockedUserInfo"/></returns>
        public BlockedUserInfo Create(string Reason, Guid PrevRoleId, User User)
        {
            var BlockedUserInfo = new BlockedUserInfo(Reason, PrevRoleId, User);

            //Todo создание события

            return BlockedUserInfo;
        }
    }
}
