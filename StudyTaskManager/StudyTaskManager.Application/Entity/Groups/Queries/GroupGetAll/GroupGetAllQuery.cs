using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupGetAll;

public sealed record GroupGetAllQuery(
    Expression<Func<Group, bool>>? Predicate) : IQuery<List<GroupResponse>>;