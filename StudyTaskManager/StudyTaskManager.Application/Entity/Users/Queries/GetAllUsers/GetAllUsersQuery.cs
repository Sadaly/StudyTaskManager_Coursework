using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record GetAllUsersQuery() : IQuery<UserListResponse>;