using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageMarkAsRead;

public sealed record PersonalMessageMarkAsReadCommand(
    Guid PersonalMessageId) : ICommand;
