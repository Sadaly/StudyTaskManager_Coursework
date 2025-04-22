using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;

public sealed record GroupGetByIdResponse(
    string Title,
    string? Description,
    Guid DefaultRoleId)
{
    internal GroupGetByIdResponse(Group gropu)
        : this(
              gropu.Title.Value,
              gropu.Description?.Value,
              gropu.DefaultRoleId)
    { }
}