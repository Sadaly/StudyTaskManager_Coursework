using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageUpdateContent;

public sealed record PersonalMessageUpdateContentCommand(
    Guid PersonalMessageId,
    string Content) : ICommand;
