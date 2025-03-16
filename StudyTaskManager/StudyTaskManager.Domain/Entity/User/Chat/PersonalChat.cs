using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.User.Chat
{
    /// <summary>
    /// Личный чат между двумя пользователями
    /// </summary>
    public class PersonalChat : BaseEntityWithID
    {
        /// <summary>
        /// Id пользователя 1
        /// </summary>
        public Guid UserId1 { get; }

        /// <summary>
        /// Id пользователя 2
        /// </summary>
        public Guid UserId2 { get; }



        /// <summary>
        /// Пользователь 1
        /// </summary>
        public AbsUser User1 { get; } = null!;

        /// <summary>
        /// Пользователь 2
        /// </summary>
        public AbsUser User2 { get; } = null!;

        /// <summary>
        /// Перечисление сообщений из личных чатов
        /// </summary>
        public IReadOnlyCollection<PersonalMessage>? Messages => _messages;
        private List<PersonalMessage>? _messages;
    }
}
