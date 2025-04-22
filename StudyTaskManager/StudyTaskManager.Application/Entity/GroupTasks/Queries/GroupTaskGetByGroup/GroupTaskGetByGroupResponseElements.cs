using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup;

public sealed record GroupTaskGetByGroupResponseElements(
    Guid Id,
    Guid? ParentId,
    DateTime DateCreated,
    DateTime Deadline,
    Guid StatusId,
    string HeadLine,
    string? Description,
    Guid? ResponsibleId)
{
    internal GroupTaskGetByGroupResponseElements(GroupTask task)
        : this(
             task.Id,
             task.ParentId,
             task.DateCreated,
             task.Deadline,
             task.StatusId,
             task.HeadLine.Value,
             task.Description?.Value,
             task.ResponsibleId)
    { }
}