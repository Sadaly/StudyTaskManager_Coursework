using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.GroupRoles.Queries.GroupRoleGetAll;

public sealed record GroupRoleGetAllQuery(
    Expression<Func<GroupRole, bool>> Perdicate) : IQuery<List<GroupRoleResponse>>;