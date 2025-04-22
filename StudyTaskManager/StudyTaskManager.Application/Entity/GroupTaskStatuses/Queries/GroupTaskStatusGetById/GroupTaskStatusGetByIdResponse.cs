using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetById;

public sealed record GroupTaskStatusGetByIdResponse(
    string Name,
    Guid? GroupId,
    bool CanBeUpdated,
    string? Description)
{
    internal GroupTaskStatusGetByIdResponse(GroupTaskStatus status)
        : this(
             status.Name.Value,
             status.GroupId,
             status.CanBeUpdated,
             status.Description?.Value)
    { }
}
