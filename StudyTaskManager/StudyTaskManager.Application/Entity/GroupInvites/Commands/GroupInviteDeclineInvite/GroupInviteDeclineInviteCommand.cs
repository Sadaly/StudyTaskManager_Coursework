using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteDeclineInvite;

public sealed record GroupInviteDeclineInviteCommand(
    Guid ReceiverId,
    Guid GroupId) : ICommand;