namespace StudyTaskManager.WebAPI.Contracts.Users
{
    public sealed record CreateUserRequest(
        string Username,
        string Email,
        string Password,
        string? PhoneNumber,
        Guid? SystemRole
        );
}