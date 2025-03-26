using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.WebAPI.Contracts.Users
{
    public sealed record RegisterUserRequest(
        string Username,
        string Email,
        string Password,
        string? PhoneNumber,
        Guid? SystemRole
        );
}