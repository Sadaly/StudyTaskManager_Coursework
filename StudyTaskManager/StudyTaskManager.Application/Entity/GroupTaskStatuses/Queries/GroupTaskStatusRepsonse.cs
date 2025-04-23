using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries;

public sealed record GroupTaskStatusRepsonse(
    Guid Id,
    string Name,
    Guid? GroupId,
    bool CanBeUpdated,
    string? Description)
{
    internal GroupTaskStatusRepsonse(GroupTaskStatus status)
        : this(
             status.Id,
             status.Name.Value,
             status.GroupId,
             status.CanBeUpdated,
             status.Description?.Value)
    { }
}
