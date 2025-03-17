using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Роль пользователей в группе
    /// </summary>
    public class GroupRole : BaseEntityWithID
    {
        /// <summary>
        /// Указывает на id группы, в которой эта роль была создана, для базовых ролей используется значение null
        /// </summary>
        public Guid? GroupId { get; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; } = null!;

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
