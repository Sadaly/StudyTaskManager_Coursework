using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById;

public sealed record SystemRoleGetByIdQuery(
    Guid SystemRoleId) : IQuery<SystemRoleGetByIdResponse>;
