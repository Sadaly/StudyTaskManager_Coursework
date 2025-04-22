using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries.GetAllUsers;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById;

public sealed record GetAllUsersQuery(
    int StartIndex,
    int Count) : IQuery<List<GetAllUsersResponseElements>>;