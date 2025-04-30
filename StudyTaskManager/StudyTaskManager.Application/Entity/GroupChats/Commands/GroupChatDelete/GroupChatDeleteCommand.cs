using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatDelete;

public sealed record GroupChatDeleteCommand(
    Guid ChatId) : ICommand;