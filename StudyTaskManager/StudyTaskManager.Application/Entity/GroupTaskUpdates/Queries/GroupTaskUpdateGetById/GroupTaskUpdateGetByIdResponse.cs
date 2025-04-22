using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetById;

public sealed record GroupTaskUpdateGetByIdResponse(
    Guid TaskId,
    DateTime DateCreated,
    string Content)
{
    internal GroupTaskUpdateGetByIdResponse(GroupTaskUpdate update)
        : this(
             update.TaskId,
             update.DateCreated,
             update.Content.Value)
    { }
}
