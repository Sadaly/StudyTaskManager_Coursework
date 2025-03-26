using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Группа, объединяющая пользователей для выполнения задач и общения.
    /// </summary>
    public class Group : BaseEntityWithID
    {
        private Group(Guid id, Title title, Content? description, Guid defaultRoleId) : base(id)
        {
            Title = title;
            Description = description;

            DefaultRoleId = defaultRoleId;

            _usersInGroup = [];
            _groupRoles = [];
            _groupInvites = [];
        }

        #region свойства

        /// <summary>
        /// Название группы (обязательно).
        /// </summary>
        public Title Title { get; private set; }

        /// <summary>
        /// Описание группы (может быть пустым).
        /// </summary>
        public Content? Description { get; private set; }

        /// <summary>
        /// ID роли по умолчанию для новых пользователей.
        /// </summary>
        public Guid DefaultRoleId { get; private set; }

        /// <summary>
        /// Роль по умолчанию для новых пользователей.
        /// </summary>
        public GroupRole? DefaultRole { get; private set; }

        /// <summary>
        /// Пользователи в группе.
        /// </summary>
        public List<UserInGroup> UsersInGroup => _usersInGroup;
        private readonly List<UserInGroup> _usersInGroup;

        /// <summary>
        /// Роли в группе.
        /// </summary>
        public List<GroupRole> GroupRoles => _groupRoles;
        private readonly List<GroupRole> _groupRoles;

        /// <summary>
        /// Приглашения в группу.
        /// </summary>
        public List<GroupInvite> GroupInvites => _groupInvites;
        private readonly List<GroupInvite> _groupInvites;

        #endregion

        /// <summary>
        /// Создает новую группу.
        /// </summary>
        public static Group Create(Guid id, Title name, Content? description, GroupRole defaultRole)
        {
            return new Group(id, name, description, defaultRole.Id);
        }

        /// <summary>
        /// Добавляет пользователя в группу.
        /// </summary>
        public Result AddUserToGroup(User.User user, GroupRole role)
        {
            if (_usersInGroup.Any(u => u.UserId == user.Id))
                return Result.Failure(DomainErrors.Group.UserAlreadyInGroup);

            _usersInGroup.Add(UserInGroup.Create(this, role, user));

            return Result.Success();

        }

        /// <summary>
        /// Удаляет пользователя из группы.
        /// </summary>
        public Result RemoveUserFromGroup(Guid userId)
        {
            var userInGroup = _usersInGroup.FirstOrDefault(u => u.UserId == userId);
            if (userInGroup == null)
                return Result.Failure(DomainErrors.Group.UserNotFound);

            _usersInGroup.Remove(userInGroup);

            return Result.Success();
        }

        /// <summary>
        /// Добавляет новую роль в группу.
        /// </summary>
        public Result AddRole(GroupRole role)
        {
            if (_groupRoles.Any(r => r.Id == role.Id))
                return Result.Failure(DomainErrors.Group.RoleAlreadyExists);

            _groupRoles.Add(role);

            return Result.Success();
        }

        /// <summary>
        /// Удаляет роль из группы.
        /// </summary>
        public Result RemoveRole(GroupRole role)
        {
            if (role == null)
                return Result.Failure(DomainErrors.Group.RoleNotFound);

            if (role.Group == null)
                return Result.Failure(DomainErrors.Group.CantDeleteBaseRole);

            _groupRoles.Remove(role);

            return Result.Success();
        }

        /// <summary>
        /// Добавляет приглашение в группу.
        /// </summary>
        public Result AddInvite(GroupInvite invite)
        {
            if (_groupInvites.Any(i => i.ReceiverId == invite.ReceiverId))
                return Result.Failure(DomainErrors.Group.InviteAlreadySent);

            _groupInvites.Add(invite);

            return Result.Success();
        }

        /// <summary>
        /// Удаляет приглашение из группы.
        /// </summary>
        public Result RemoveInvite(GroupInvite invite)
        {
            if (!_groupInvites.Any(i => i == invite))
                return Result.Failure(DomainErrors.Group.InviteNotFound);

            _groupInvites.Remove(invite);

            return Result.Success();
        }
    }
}
