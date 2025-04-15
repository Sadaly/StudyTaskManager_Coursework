using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdateTitle;

public sealed record SystemRoleUpdateTitleCommand(
    Guid SystemRoleId,
    string NewTitle) : ICommand;
