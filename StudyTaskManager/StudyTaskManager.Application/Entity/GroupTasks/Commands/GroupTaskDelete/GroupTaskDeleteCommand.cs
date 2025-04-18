using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskDelete;

public sealed record GroupTaskDeleteCommand(Guid Id) : ICommand;