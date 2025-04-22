using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetByGroupWithBase;

public sealed record GroupTaskStatusGetByGroupWithBaseRepsonseElements(
    Guid Id,
    string Name,
    Guid? GroupId,
    bool CanBeUpdated,
    string? Description)
{
    internal GroupTaskStatusGetByGroupWithBaseRepsonseElements(GroupTaskStatus status)
        : this(
             status.Id,
             status.Name.Value,
             status.GroupId,
             status.CanBeUpdated,
             status.Description?.Value)
    { }
}
