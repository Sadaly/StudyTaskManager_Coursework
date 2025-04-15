using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete;

public sealed record UserDeleteCommand(
    Guid UserId) : DeleteByIdCommand<StudyTaskManager.Domain.Entity.User.User>(UserId);
