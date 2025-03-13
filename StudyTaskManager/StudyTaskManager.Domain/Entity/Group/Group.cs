using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Группа, объединяющая пользователь для выполнения задач и общения
    /// </summary>
    public class Group : BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Название группы (not null)
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание группы. Может быть пустым
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Id роли по умолчанию
        /// </summary>
        public int DefaultRoleId { get; set; }



        /// <summary>
        /// Роль по умолчанию. Для новых созданных групп будет установлена базовая роль
        /// </summary>
        public GroupRole DefaultRole { get; set; } = null!;

        /// <summary>
        /// Перечисление пользователей в группе
        /// </summary>
        public IReadOnlyCollection<UserInGroup>? UsersInGroup => _usersInGroup;
        private List<UserInGroup>? _usersInGroup;

        /// <summary>
        /// Перечисление ролей в группе
        /// </summary>
        public IReadOnlyCollection<GroupRole>? GroupRoles => _groupRoles;
        private List<GroupRole>? _groupRoles;

        /// <summary>
        /// Перечисление приглашений в группу
        /// </summary>
        public IReadOnlyCollection<GroupInvite>? GroupInvites => _groupInvites;
        private List<GroupInvite>? _groupInvites;

        /// <summary>
        /// Перечисление логов
        /// </summary>
        public IReadOnlyCollection<Log.Log>? GroupLogs => _groupLogs;
        private List<Log.Log>? _groupLogs;

    }
}
