using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Task
{
    /// <summary>
    /// Статус задачи в группе.
    /// </summary>
    public class GroupTaskStatus : BaseEntityWithID
    {
        private GroupTaskStatus(Guid id, bool canBeUpdated, Guid? groupId) : base(id)
        {
            CanBeUpdated = canBeUpdated;
            GroupId = groupId;
        }
        private GroupTaskStatus(Guid id, bool canBeUpdated, Guid? groupId, Title name, Content? description) : this(id, canBeUpdated, groupId)
        {
            Name = name;
            Description = description;
        }

        #region свойства

        /// <summary>
        /// Название статуса задачи.
        /// </summary>
        public Title Name { get; set; } = null!;

        /// <summary>
        /// Id группы, к которой относится этот статус.
        /// </summary>
        public Guid? GroupId { get; }

        /// <summary>
        /// Может ли статус задачи быть обновлен.
        /// </summary>
        public bool CanBeUpdated { get; set; }

        /// <summary>
        /// Ссылка на группу.
        /// </summary>
        public Group? Group { get; private set; }

        /// <summary>
        /// Описание статуса.
        /// </summary>
        public Content? Description { get; set; }

        #endregion

        /// <summary>
        /// Статический метод для создания нового статуса задачи.
        /// </summary>
        public static GroupTaskStatus Create(Guid id, Title name, bool canBeUpdated, Group? group, Content? description)
        {
            return new GroupTaskStatus(id, canBeUpdated, group?.Id, name, description)
            {
                Group = group
            };
        }
    }
}
