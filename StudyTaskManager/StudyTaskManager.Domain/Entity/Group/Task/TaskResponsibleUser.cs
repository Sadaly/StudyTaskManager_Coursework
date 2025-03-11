namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Пользователь ответственный за задачу
    /// </summary>
    public class TaskResponsibleUser
    {
        /// <summary>
        /// Id ответственного пользователя
        /// </summary>
        public int ResponsibleUserId { get; set; }

        /// <summary>
        /// Id группы
        /// </summary>
        public int GroupId { get; }

        /// <summary>
        /// Id задачи
        /// </summary>
        public int TaskId { get; }



        /// <summary>
        /// ССылка на ответственного пользователя
        /// </summary>
        public User.AbsUser? ResponsibleUser { get; set; }

        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group Group { get; } = null!;

        /// <summary>
        /// Ссылка на задачу
        /// </summary>
        public Task Task { get; } = null!;
    }
}
