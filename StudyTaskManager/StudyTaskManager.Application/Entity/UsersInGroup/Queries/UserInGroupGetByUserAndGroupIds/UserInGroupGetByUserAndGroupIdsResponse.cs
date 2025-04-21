using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds;

public sealed record UserInGroupGetByUserAndGroupIdsResponse(Guid RoleId, DateTime DateEntered);