using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries.SystemRoleGetAll;

public sealed record SystemRoleGetAllQuery(
    Expression<Func<SystemRole, bool>>? Predicate) : IQuery<List<SystemRoleResponse>>;