using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.Users.Queries.TakeUsers;
public sealed record TakeUsersQuery(
    int StartIndex,
    int Count,
    Expression<Func<User, bool>>? Predicate) : IQuery<List<UserResponse>>;