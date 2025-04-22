using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries.UserGetById;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record UserGetByIdQuery(Guid UserId) : IQuery<UserGetByIdResponse>;