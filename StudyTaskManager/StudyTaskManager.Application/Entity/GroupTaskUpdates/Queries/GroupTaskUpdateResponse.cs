using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries;

public sealed record GroupTaskUpdateResponse(
    Guid Id,
    Guid TaskId,
    DateTime DateCreated,
    string Content)
{
    internal GroupTaskUpdateResponse(GroupTaskUpdate update)
        : this(
             update.Id,
             update.TaskId,
             update.DateCreated,
             update.Content.Value)
    { }
}