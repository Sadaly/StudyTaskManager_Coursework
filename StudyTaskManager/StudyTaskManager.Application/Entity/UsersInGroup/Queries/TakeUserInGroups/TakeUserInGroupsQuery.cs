using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.TakeUserInGroups;
public sealed record TakeUserInGroupsQuery(
    int StartIndex,
    int Count,
    Expression<Func<UserInGroup, bool>>? Predicate) : IQuery<List<UserInGroupsResponse>>;