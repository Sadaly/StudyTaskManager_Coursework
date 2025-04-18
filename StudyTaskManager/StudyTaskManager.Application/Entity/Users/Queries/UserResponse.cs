using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Queries;

public sealed record class UserResponse(User User);
public sealed record class UserListResponse(List<User> Users);
