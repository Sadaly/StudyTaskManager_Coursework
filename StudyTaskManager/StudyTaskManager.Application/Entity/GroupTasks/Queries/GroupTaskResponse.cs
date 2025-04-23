using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries;

public sealed record GroupTaskResponse(
    Guid Id,
    Guid GroupId,
    Guid? ParentId,
    DateTime DateCreated,
    DateTime Deadline,
    Guid StatusId,
    string HeadLine,
    string? Description,
    Guid? ResponsibleId)
{
    internal GroupTaskResponse(GroupTask task)
        : this(
             task.Id,
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