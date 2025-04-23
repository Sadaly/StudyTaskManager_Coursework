using System.Linq.Expressions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record GetAllUsersQuery(
    Expression<Func<User, bool>>? Predicate) : IQuery<List<UserResponse>>;