using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.WebAPI.Contracts.Users
{
    public sealed record RegisterUserRequest(
        string UserName,
        string Email,
        string Password,
        string? PhoneNumber,
        SystemRole? SystemRole
        );
}