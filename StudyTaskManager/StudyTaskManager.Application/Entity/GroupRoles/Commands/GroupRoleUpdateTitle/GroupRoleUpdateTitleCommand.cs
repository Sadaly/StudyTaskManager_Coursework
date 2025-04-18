using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleUpdateTitle;

public sealed record GroupRoleUpdateTitleCommand(
    Guid Id,
    string Title) : ICommand;