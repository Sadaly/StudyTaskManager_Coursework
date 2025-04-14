using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatAddMessage;

public sealed record PersonalChatAddMessageCommand(
    Guid SenderId,
    Guid ReceiverId,
    string Message) : ICommand<Guid>;
