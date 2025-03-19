using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Статус задачи
    /// </summary>
    public class GroupTaskStatus : BaseEntityWithID
    {
        /// <summary>
        /// Название
        /// </summary>
        public Title Name { get; set; } = null!;

        /// <summary>
        /// Id группы
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Может ли статус быть обновлен
        /// </summary>
        public bool Can_Be_Updated { get; set; }



        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public Group? Group { get; }
    }
}
