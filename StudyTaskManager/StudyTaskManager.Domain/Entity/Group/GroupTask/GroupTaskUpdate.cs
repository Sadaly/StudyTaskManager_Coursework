using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    //Todo
    public class GroupTaskUpdate : BaseEntityWithID
    {
        /// <summary>
        /// Id создателя апдейта
        /// </summary>
        public Guid CreatorId { get; }

        /// <summary>
        /// Id задачи
        /// </summary>
        public Guid TaskId { get; }

        /// <summary>
        /// Дата создания апдейта
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Содержание
        /// </summary>
        public string Content { get; } = null!;



        /// <summary>
        /// Ссылка на создателя апдейта
        /// </summary>
        public User.AbsUser Creator { get; } = null!;

        /// <summary>
        /// Ссылка на задачу
        /// </summary>
        public GroupTask Task { get; } = null!;
    }
}
