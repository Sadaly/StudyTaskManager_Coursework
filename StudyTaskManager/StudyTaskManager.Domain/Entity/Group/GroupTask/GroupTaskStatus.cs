using MediatR;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;
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
        public static Result<GroupTaskStatus> Create(Guid id, Title name, bool canBeUpdated, Group? group, Content? description)
        {
            var gts = new GroupTaskStatus(id, canBeUpdated, group?.Id, name, description)
            {
                Group = group
            };

            gts.RaiseDomainEvent(new GroupTaskStatusCreatedDomainEvent(gts.Id));

            return Result.Success(gts);
        }

        public Result Update(Title? name, bool? canBeUpdated, Content? description)
        {
            if (name is null && canBeUpdated is null)
                return Result.Failure(new Error(
                    "GroupTaskStatus.NothingToUpdate",
                    $"Никакие из полей {this.Id} не были затронуты"));

            if (name is not null)
                Name = name;

            if (canBeUpdated is not null)
                CanBeUpdated = canBeUpdated.Value;

            Description = description;

            RaiseDomainEvent(new GroupTaskStatusUpdatedDomainEvent(Id));

            return Result.Success();
        }
    }
}
