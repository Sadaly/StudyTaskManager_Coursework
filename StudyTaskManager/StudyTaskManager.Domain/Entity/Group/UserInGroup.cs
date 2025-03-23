namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Представляет пользователя в группе с определенной ролью.
    /// </summary>
    public class UserInGroup : Common.BaseEntity
    {
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="UserInGroup"/>.
        /// </summary>
        /// <param name="group">Группа, в которую входит пользователь.</param>
        /// <param name="role">Роль пользователя в группе.</param>
        /// <param name="user">Пользователь, состоящий в группе.</param>
        private UserInGroup(Guid groupId, Guid roleId, Guid userId) : base()
        {
            UserId = userId;
            RoleId = roleId;
            GroupId = groupId;

            DateEntered = DateTime.UtcNow;
        }

        #region свойства

        public Guid GroupId { get; }
        public Guid RoleId { get; private set; }
        public Guid UserId { get; }
        public DateTime DateEntered { get; }
        public Group? Group { get; private set; }
        public GroupRole? Role { get; private set; }
        public User.User? User { get; private set; }

        #endregion

        /// <summary>
        /// Изменяет роль пользователя в группе.
        /// </summary>
        /// <param name="newRole">Новая роль пользователя.</param>
        public void ChangeRole(GroupRole newRole)
        {
            Role = newRole;
            RoleId = newRole.Id;
            // TODO: Добавить логику для записи события смены роли
        }

        /// <summary>
        /// Создает новый объект <see cref="UserInGroup"/>.
        /// </summary>
        /// <param name="group">Группа, в которую входит пользователь.</param>
        /// <param name="role">Роль пользователя в группе.</param>
        /// <param name="user">Пользователь.</param>
        /// <returns>Новый экземпляр <see cref="UserInGroup"/>.</returns>
        public static UserInGroup Create(Group group, GroupRole role, User.User user)
        {
            var userInGroup = new UserInGroup(group.Id, role.Id, user.Id)
            {
                Group = group,
                Role = role,
                User = user
            };

            // TODO: Добавить создание доменного события о вступлении пользователя в группу.

            return userInGroup;
        }
    }
}
