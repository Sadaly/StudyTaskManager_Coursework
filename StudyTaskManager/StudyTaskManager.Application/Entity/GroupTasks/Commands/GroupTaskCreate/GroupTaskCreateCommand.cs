using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskCreate;

public sealed record GroupTaskCreateCommand(
    Guid GroupId,
    DateTime Deadline,
    Guid StatusId,
    string HeadLine,
    string? Description,
    Guid? ResponsibleUserId,
    Guid? ParentTaskId) : ICommand<Guid>;