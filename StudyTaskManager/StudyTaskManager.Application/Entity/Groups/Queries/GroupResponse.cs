using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.Groups.Queries;

public sealed record GroupResponse(
    Guid id,
    string Title,
    string? Description,
    Guid DefaultRoleId)
{
    internal GroupResponse(Group group)
        : this(
              group.Id,
              group.Title.Value,
              group.Description?.Value,
              group.DefaultRoleId)
    { }
}