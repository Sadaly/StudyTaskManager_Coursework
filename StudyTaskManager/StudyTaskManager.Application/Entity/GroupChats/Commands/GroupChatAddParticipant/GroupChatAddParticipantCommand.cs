using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatAddParticipant;

public sealed record GroupChatAddParticipantCommand(
    Guid GroupChatId,
    Guid UserId
    ) : ICommand;