using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteDelete;

public sealed record GroupInviteDeleteCommand(
    Guid ReceiverId,
    Guid GroupId) : ICommand;