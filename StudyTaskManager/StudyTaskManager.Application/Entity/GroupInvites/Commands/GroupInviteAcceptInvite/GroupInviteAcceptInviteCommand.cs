using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteAcceptInvite;

public sealed record GroupInviteAcceptInviteCommand(
    Guid ReceiverId,
    Guid GroupId) : ICommand;