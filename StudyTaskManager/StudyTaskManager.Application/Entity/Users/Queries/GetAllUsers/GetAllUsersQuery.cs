using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record GetAllUsersQuery() : IQuery<List<Domain.Entity.User.User>>;