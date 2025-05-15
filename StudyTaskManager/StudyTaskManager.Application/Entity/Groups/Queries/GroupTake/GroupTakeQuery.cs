using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupTake;

public sealed record GroupTakeQuery(
    int StartIndex,
    int Count,
    Expression<Func<Group, bool>>? Predicate) : IQuery<List<GroupResponse>>;