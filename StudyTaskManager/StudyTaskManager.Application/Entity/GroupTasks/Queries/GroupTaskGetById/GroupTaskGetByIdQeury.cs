using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById;

public sealed record GroupTaskGetByIdQeury(Guid Id) : IQuery<GroupTask>;