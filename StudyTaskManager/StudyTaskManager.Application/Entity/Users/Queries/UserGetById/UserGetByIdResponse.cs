using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Queries.UserGetById;

public sealed record UserGetByIdResponse(
    string Username,
    string Email,
    bool IsEmailVerifed,
    bool IsPhoneNumberVerifed,
    string? PhoneNumber,
    DateTime RegistrationDate,
    Guid? SystemRoleId)
{
    internal UserGetByIdResponse(User user)
        : this(user.Username.Value, user.Email.Value,
              user.IsEmailVerifed, user.IsPhoneNumberVerifed,
              user.PhoneNumber?.Value, user.RegistrationDate, user.SystemRoleId)
    { }
}