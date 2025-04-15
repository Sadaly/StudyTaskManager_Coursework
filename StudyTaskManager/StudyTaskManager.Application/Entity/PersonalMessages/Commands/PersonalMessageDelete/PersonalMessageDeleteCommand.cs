using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;

public sealed record PersonalMessageDeleteCommand(
    Guid PersonalMessageId) : DeleteByIdCommand<PersonalMessage>(PersonalMessageId);
