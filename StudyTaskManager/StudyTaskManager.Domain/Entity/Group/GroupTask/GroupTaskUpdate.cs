using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.GroupTask
{
    /// <summary>
    /// Обновление задачи в группе.
    /// </summary>
    public class GroupTaskUpdate : BaseEntityWithID
    {
        private GroupTaskUpdate(Guid id, User.User creator, GroupTask task, Content content) : base(id)
        {
            Creator = creator;
            Task = task;
            Content = content;

            CreatorId = creator.Id;
            TaskId = task.Id;
            DateCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Id создателя апдейта.
        /// </summary>
        public Guid CreatorId { get; }

        /// <summary>
        /// Id задачи.
        /// </summary>
        public Guid TaskId { get; }

        /// <summary>
        /// Дата создания апдейта.
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Содержание обновления задачи.
        /// </summary>
        public Content Content { get; private set; }

        /// <summary>
        /// Ссылка на создателя апдейта.
        /// </summary>
        public User.User Creator { get; }

        /// <summary>
        /// Ссылка на задачу.
        /// </summary>
        public GroupTask Task { get; }

        /// <summary>
        /// Создает новое обновление задачи.
        /// </summary>
        public static GroupTaskUpdate Create(Guid id, User.User creator, GroupTask task, Content content)
        {
            //Todo добавить событие

            return new GroupTaskUpdate(id, creator, task, content);
        }

        /// <summary>
        /// Обновляет содержание задачи.
        /// </summary>
        public void UpdateContent(Content newContent)
		{
			//Todo добавить событие
			Content = newContent;
        }
    }
}
