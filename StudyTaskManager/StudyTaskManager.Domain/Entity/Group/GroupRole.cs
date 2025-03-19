using StudyTaskManager.Domain.Common;
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
            Group = group;
            if (group != null)
            {
                GroupId = group.Id;
            }

            RoleName = roleName;
            CanCreateTasks = canCreateTasks;
            CanManageRoles = canManageRoles;
            CanCreateTaskUpdates = canCreateTaskUpdates;
            CanChangeTaskUpdates = canChangeTaskUpdates;
            CanInviteUsers = canInviteUsers;
        }

        /// <summary>
        /// Уникальный идентификатор группы, если роль привязана к группе (null для базовых ролей).
        /// </summary>
        public Guid? GroupId { get; }

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

        /// <summary>
        /// Группа, к которой привязана роль (null для базовых ролей).
        /// </summary>
        public Group? Group { get; }

        /// <summary>
        /// Метод для обновления прав роли.
        /// </summary>
        public void UpdatePermissions(bool canCreateTasks, bool canManageRoles, bool canCreateTaskUpdates, bool canChangeTaskUpdates, bool canInviteUsers)
        {
            CanCreateTasks = canCreateTasks;
            CanManageRoles = canManageRoles;
            CanCreateTaskUpdates = canCreateTaskUpdates;
            CanChangeTaskUpdates = canChangeTaskUpdates;
            CanInviteUsers = canInviteUsers;

            // TODO: Добавить событие изменения прав роли.
        }

        /// <summary>
        /// Создает новую роль в группе.
        /// </summary>
        public static GroupRole Create(Guid id, Title roleName, bool canCreateTasks, bool canManageRoles, bool canCreateTaskUpdates, bool canChangeTaskUpdates, bool canInviteUsers, Group? group)
        {
            var groupRole = new GroupRole(id, roleName, canCreateTasks, canManageRoles, canCreateTaskUpdates, canChangeTaskUpdates, canInviteUsers, group);

            // TODO: Добавить событие создания роли.

            return groupRole;
        }
    }
}
