using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetById;

public sealed record GroupRoleGetByIdQuery(Guid Id) : IQuery<GroupRoleResponse>;