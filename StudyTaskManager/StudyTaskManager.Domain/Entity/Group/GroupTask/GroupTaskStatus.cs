using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Статус задачи в группе.
    /// </summary>
    public class GroupTaskStatus : BaseEntityWithID
    {
        // Приватный конструктор для создания статуса
        private GroupTaskStatus(Guid id, Title name, bool canBeUpdated, Group? group) : base(id)
        {
            Name = name;
            Can_Be_Updated = canBeUpdated;
            Group = group;

            if (group != null)
            {
                GroupId = group.Id;
            }
        }

        /// <summary>
        /// Название статуса задачи.
        /// </summary>
        public Title Name { get; set; } = null!;

        /// <summary>
        /// Id группы, к которой относится этот статус.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Может ли статус задачи быть обновлен.
        /// </summary>
        public bool Can_Be_Updated { get; set; }

        /// <summary>
        /// Ссылка на группу.
        /// </summary>
        public Group? Group { get; }

        /// <summary>
        /// Статический метод для создания нового статуса задачи.
        /// </summary>
        public static GroupTaskStatus Create(Guid id, Title name, Guid groupId, bool canBeUpdated, Group group)
		{
			//Todo добавить событие
			return new GroupTaskStatus(id, name, canBeUpdated, group);
        }
    }
}
