using Microsoft.VisualBasic;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Роль пользователя в группе.
    /// </summary>
    public class GroupRole : BaseEntityWithID
    {
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="GroupRole"/>.
        /// </summary>
        private GroupRole(Guid id, Title roleName, bool canCreateTasks, bool canManageRoles, bool canCreateTaskUpdates, bool canChangeTaskUpdates, bool canInviteUsers, Group? group) : base(id)
        {
            if (group != null)
            {
                Group = group;
                GroupId = group?.Id;
            }
            RoleName = roleName;
            CanCreateTasks = canCreateTasks;
            CanManageRoles = canManageRoles;
            CanCreateTaskUpdates = canCreateTaskUpdates;
            CanChangeTaskUpdates = canChangeTaskUpdates;
            CanInviteUsers = canInviteUsers;
        }

        public GroupRole()
        {
            RoleName = Title.Create("RoleName").Value;
        }

        #region свойства

        /// <summary>
        /// Уникальный идентификатор группы, если роль привязана к группе (null для ролей общих).
        /// </summary>
        public Guid? GroupId { get; }
        /// <summary>
        /// Группа, к которой привязана роль (если нет группы, то роль общая).
        /// </summary>
        public Group? Group { get; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public Title RoleName { get; set; }

        /// <summary>
        /// Может ли пользователь создавать задачи.
        /// </summary>
        public bool CanCreateTasks { get; private set; }
        /// <summary>
        /// Может ли пользователь управлять ролями (выдавать, создавать, удалять, изменять).
        /// </summary>
        public bool CanManageRoles { get; private set; }
        /// <summary>
        /// Может ли пользователь создавать обновления к задачам.
        /// </summary>
        public bool CanCreateTaskUpdates { get; private set; }
        /// <summary>
        /// Может ли пользователь изменять обновления к задачам.
        /// </summary>
        public bool CanChangeTaskUpdates { get; private set; }
        /// <summary>
        /// Может ли пользователь приглашать в группу других пользователей.
        /// </summary>
        public bool CanInviteUsers { get; private set; }

        #endregion

        /// <summary>
        /// Метод для обновления прав роли.
        /// </summary>
        public Result UpdatePermissions(bool canCreateTasks, bool canManageRoles, bool canCreateTaskUpdates, bool canChangeTaskUpdates, bool canInviteUsers)
        {
            CanCreateTasks = canCreateTasks;
            CanManageRoles = canManageRoles;
            CanCreateTaskUpdates = canCreateTaskUpdates;
            CanChangeTaskUpdates = canChangeTaskUpdates;
            CanInviteUsers = canInviteUsers;

            RaiseDomainEvent(new GroupRolePermisionsUpdatedDomainEvent(Id));

            return Result.Success();
        }

        /// <summary>
        /// Создает новую роль в группе.
        /// </summary>
        public static Result<GroupRole> Create(Title roleName, bool canCreateTasks, bool canManageRoles, bool canCreateTaskUpdates, bool canChangeTaskUpdates, bool canInviteUsers, Group? group)
        {
            var groupRole = new GroupRole(Guid.Empty, roleName, canCreateTasks, canManageRoles, canCreateTaskUpdates, canChangeTaskUpdates, canInviteUsers, group);

            groupRole.RaiseDomainEvent(new GroupRoleCreatedDomainEvent(groupRole.Id));

            return Result.Success(groupRole);
        }
    }
}
