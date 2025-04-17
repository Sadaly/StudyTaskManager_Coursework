
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Commands.GroupTaskDelete;

public sealed record GroupTaskDeleteCommand(Guid Id) : DeleteByIdCommand<GroupTask>(Id);