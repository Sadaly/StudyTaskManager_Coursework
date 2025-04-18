using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetById;

public sealed record SystemRoleGetByIdQuery(
    Guid SystemRoleId) : IQuery<SystemRoleResponse>;
