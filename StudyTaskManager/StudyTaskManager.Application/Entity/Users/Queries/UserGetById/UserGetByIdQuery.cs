using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record UserGetByIdQuery(Guid UserId) : IQuery<UserResponse>;