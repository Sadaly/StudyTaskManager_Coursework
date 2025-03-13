using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    //Todo
    public class GroupTaskUpdate : BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Id создателя апдейта
        /// </summary>
        public int CreatorId { get; }

        /// <summary>
        /// Id задачи
        /// </summary>
        public int TaskId { get; }

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
