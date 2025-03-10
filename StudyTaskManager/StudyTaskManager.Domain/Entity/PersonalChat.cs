using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    public class PersonalChat : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Id пользователя 1
        /// </summary>
        public int UserId1 { get; }

        /// <summary>
        /// Id пользователя 2
        /// </summary>
        public int UserId2 { get; }



        /// <summary>
        /// Пользователь 1
        /// </summary>
        public User User1 { get; } = null!;

        /// <summary>
        /// Пользователь 2
        /// </summary>
        public User User2 { get; } = null!;

        /// <summary>
        /// Перечисление сообщений из личных чатов
        /// </summary>
        public IReadOnlyCollection<PersonalMessage>? Messages => _messages;
        private List<PersonalMessage>? _messages;
    }
}
