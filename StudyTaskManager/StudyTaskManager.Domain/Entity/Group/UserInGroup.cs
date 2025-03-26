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
        private UserInGroup(Guid groupId, Guid roleId, Guid userId) : base()
        {
            UserId = userId;
            RoleId = roleId;
            GroupId = groupId;

            DateEntered = DateTime.UtcNow;
        }

        #region свойства

        /// <summary>
        /// Уникальный идентификатор группы.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Уникальный идентификатор роли пользователя в группе.
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Дата и время вступления пользователя в группу.
        /// </summary>
        public DateTime DateEntered { get; }

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
        public static Result<UserInGroup> Create(Group group, GroupRole role, User.User user)
        {
            var userInGroup = new UserInGroup(group.Id, role.Id, user.Id)
            {
                Group = group,
                Role = role,
                User = user
            };

			userInGroup.RaiseDomainEvent(new GroupUserJoinedDomainEvent(userInGroup.UserId, userInGroup.GroupId));

			return Result.Success(userInGroup);
        }

		public Result LeaveGroup()
		{
			this.RaiseDomainEvent(new GroupUserLeftDomainEvent(this.UserId, this.GroupId));

            return Result.Success();
        }

		public Result UpdateRole(GroupRole role)
		{
            this.Role = role;

			this.RaiseDomainEvent(new GroupUserRoleUpdatedDomainEvent(this.UserId, this.GroupId));

            return Result.Success();
        }
	}
}
