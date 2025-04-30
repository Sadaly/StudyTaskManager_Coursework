using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatAddMessage;

public sealed record GroupChatAddMessageCommand(
    Guid GroupChatId,
    ulong Ordinal,
    Guid SenderId,
    string Content
    ) : ICommand;
