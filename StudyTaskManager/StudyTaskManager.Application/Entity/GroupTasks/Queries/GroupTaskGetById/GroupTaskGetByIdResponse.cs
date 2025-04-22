using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById;

public sealed record GroupTaskGetByIdResponse(
    Guid GroupId,
    Guid? ParentId,
    DateTime DateCreated,
    DateTime Deadline,
    Guid StatusId,
    string HeadLine,
    string? Description,
    Guid? ResponsibleId)
{
    internal GroupTaskGetByIdResponse(GroupTask task)
        : this(
             task.GroupId,
             task.ParentId,
             task.DateCreated,
             task.Deadline,
             task.StatusId,
             task.HeadLine.Value,
             task.Description?.Value,
             task.ResponsibleId)
    { }
}
