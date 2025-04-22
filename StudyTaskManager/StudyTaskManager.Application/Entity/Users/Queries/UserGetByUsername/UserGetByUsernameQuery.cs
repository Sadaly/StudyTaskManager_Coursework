using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Queries.UserGetByUsername;

public sealed record UserGetByUsernameQuery(string Username) : IQuery<UserGetByUsernameResponse>;