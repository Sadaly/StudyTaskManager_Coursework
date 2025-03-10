namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Роли пользователей в группе
    /// </summary>
    public class GroupRole
    {
        /// <summary>
        /// Указывает на группы, в которых эта роль была создана, для базовых ролей используется значение null
        /// </summary>
        public Group? Group { get; set; }
        public string RoleName { get; set; } = null!;
        public bool Can_Create_Tasks { get; set; }
        public bool Can_Manage_Roles { get; set; }
        public bool Can_Create_Task_Updates { get; set; }
        public bool Can_Change_Task_Updates { get; set; }
        public bool Can_Invite_Users { get; set; }



    }
}
