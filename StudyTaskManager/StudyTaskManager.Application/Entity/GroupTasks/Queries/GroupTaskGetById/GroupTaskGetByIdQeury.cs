using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Queries.GetById;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById;

public sealed record GroupTaskGetByIdQeury(Guid Id) : GetByIdQuery<GroupTask>(Id);