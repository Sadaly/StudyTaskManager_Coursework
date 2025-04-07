using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

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
        private UserInGroup(Guid groupId, Guid userId, Guid roleId, DateTime dateEntered) : base()
        {
            GroupId = groupId;
            UserId = userId;
            RoleId = roleId;
            DateEntered = dateEntered;
        }

        #region свойства
        /// <summary>
        /// Уникальный идентификатор группы.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Уникальный идентификатор роли пользователя в группе.
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// Дата и время вступления пользователя в группу.
        /// </summary>
        public DateTime DateEntered { get; set; }

        /// <summary>
        /// Ссылка на группу, в которой состоит пользователь.
        /// </summary>
        public Group? Group { get; private set; }

        /// <summary>
        /// Ссылка на роль пользователя в группе.
        /// </summary>
        public GroupRole? Role { get; private set; }

        /// <summary>
        /// Ссылка на пользователя.
        /// </summary>
        public User.User? User { get; private set; }
        #endregion

        #region Create
        /// <summary>
        /// Создает новый объект <see cref="UserInGroup"/>.
        /// </summary>
        /// <param name="group">Группа, в которую входит пользователь.</param>
        /// <param name="user">Пользователь.</param>
        /// <param name="role">Роль пользователя в группе.</param>
        /// <returns>Новый экземпляр <see cref="UserInGroup"/>.</returns>
        public static Result<UserInGroup> Create(Group group, User.User user, GroupRole role)
        {
            var userInGroup = new UserInGroup(group.Id, user.Id, role.Id, DateTime.UtcNow)
            {
                Group = group,
                Role = role,
                User = user
            };

            userInGroup.RaiseDomainEvent(new GroupUserJoinedDomainEvent(userInGroup.UserId, userInGroup.GroupId));

            return Result.Success(userInGroup);
        }

        /// <summary>
        /// Создает новый объект <see cref="UserInGroup"/> с ролью в группе по умолчанию.
        /// </summary>
        /// <param name="group">Группа, в которую входит пользователь.</param>
        /// <param name="user">Пользователь.</param>
        /// <returns>Новый экземпляр <see cref="UserInGroup"/>.</returns>
        public static Result<UserInGroup> Create(Group group, User.User user)
        {
            GroupRole? role = group.DefaultRole;
            if (role != null) return Create(group, user, role);

            Guid roleId = group.DefaultRoleId
            var userInGroup = new UserInGroup(group.Id, user.Id, roleId, DateTime.UtcNow)
            {
                Group = group,
                User = user
            };

            userInGroup.RaiseDomainEvent(new GroupUserJoinedDomainEvent(userInGroup.UserId, userInGroup.GroupId));

            return Result.Success(userInGroup);
        }

        /// <summary>
        /// Создает новый объект <see cref="UserInGroup"/> на основе Id.
        /// </summary>
        public static Result<UserInGroup> Create(Guid groupId, Guid userId, Guid roleId)
        {
            var userInGroup = new UserInGroup(groupId, userId, roleId, DateTime.UtcNow);

            userInGroup.RaiseDomainEvent(new GroupUserJoinedDomainEvent(userInGroup.UserId, userInGroup.GroupId));

            return Result.Success(userInGroup);
        }
        #endregion

        public Result UpdateRole(GroupRole role)
        {
            this.Role = role;

            this.RaiseDomainEvent(new GroupUserRoleUpdatedDomainEvent(this.UserId, this.GroupId));

            return Result.Success();
        }
    }
}
