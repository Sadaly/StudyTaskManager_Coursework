using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.GetAllUserInGroups;
public sealed record UserInGroupsGetAllQuery(
    Expression<Func<UserInGroup, bool>>? Predicate) : IQuery<List<UserInGroupsResponse>>;