using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Роль пользователей в группе
    /// </summary>
    public class GroupRole : BaseEntityWithID
    {
        private GroupRole(Guid id, Title RoleName, bool Can_Create_Tasks, bool Can_Manage_Roles, bool Can_Create_Task_Updates, bool Can_Change_Task_Updates, bool Can_Invite_Users, Group? Group) : base(id)
        {
            if (Group != null)
            {
                this.GroupId = Group.Id;
                this.Group = Group;
            }
            
            this.RoleName = RoleName;
            this.Can_Create_Tasks = Can_Create_Tasks;
            this.Can_Manage_Roles = Can_Manage_Roles;
            this.Can_Create_Task_Updates = Can_Create_Task_Updates;
            this.Can_Change_Task_Updates = Can_Change_Task_Updates;
            this.Can_Invite_Users = Can_Invite_Users;
        }

        /// <summary>
        /// Указывает на id группы, в которой эта роль была создана, для базовых ролей используется значение null
        /// </summary>
        public Guid? GroupId { get; }

        /// <summary>
        /// Название роли
        /// </summary>
        public Title RoleName { get; set; } = null!;

        /// <summary>
        /// Индикатор, указывающий на то, может ли пользователь создавать задачи
        /// </summary>
        public bool Can_Create_Tasks { get; set; }

        /// <summary>
        /// Индикатор, указывающий на то, может ли управлять ролями (выдавать, создавать, удалять, изменять)
        /// </summary>
        public bool Can_Manage_Roles { get; set; }

        /// <summary>
        /// Индикатор, указывающий на то, может ли пользователь создавать апдейты к задачам
        /// </summary>
        public bool Can_Create_Task_Updates { get; set; }

        /// <summary>
        /// Индикатор, указывающий на то, может ли пользователь изменять апдейты к задачам
        /// </summary>
        public bool Can_Change_Task_Updates { get; set; }

        /// <summary>
        /// Индикатор, указывающий на то, может ли пользователь приглашать в группу других пользователей
        /// </summary>
        public bool Can_Invite_Users { get; set; }



        /// <summary>
        /// Указывает на группу, в которой эта роль была создана, для базовых ролей используется значение null
        /// </summary>
        public Group? Group { get; }
    }
}
