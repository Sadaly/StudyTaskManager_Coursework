using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageCreate;

public sealed record PersonalMessageCreateCommand(
    Guid SenderId,
    Guid PersonalChatId,
    string Content) : ICommand<Guid>;
