using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Commands.GroupTaskUpdateDelete;

public sealed record GroupTaskUpdateDeleteCommand(Guid GroupTaskUpdateId) : DeleteByIdCommand<GroupTaskUpdate>(GroupTaskUpdateId);