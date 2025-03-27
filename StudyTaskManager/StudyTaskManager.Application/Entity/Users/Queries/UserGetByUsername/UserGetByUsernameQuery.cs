using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries;

namespace StudyTaskManager.Application.Entity.User.Queries.UserGetByUsername;

public sealed record UserGetByUsernameQuery(string Username) : IQuery<UserResponse>;