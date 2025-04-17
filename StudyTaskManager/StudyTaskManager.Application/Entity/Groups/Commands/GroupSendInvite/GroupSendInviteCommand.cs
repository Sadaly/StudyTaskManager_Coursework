using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupSendInvite;

public sealed record GroupSendInviteCommand(
    Guid SenderId,
    Guid ReceiverId,
    Guid GroupId) : ICommand;