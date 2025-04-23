using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.Groups.Queries;

public sealed record GroupResponse(
    string Title,
    string? Description,
    Guid DefaultRoleId)
{
    internal GroupResponse(Group gropu)
        : this(
              gropu.Title.Value,
              gropu.Description?.Value,
              gropu.DefaultRoleId)
    { }
}