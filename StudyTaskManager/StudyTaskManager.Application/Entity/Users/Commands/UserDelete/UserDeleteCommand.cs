using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete;

public sealed record UserDeleteCommand(
    Guid UserId) : DeleteByIdCommand<User>(UserId);
