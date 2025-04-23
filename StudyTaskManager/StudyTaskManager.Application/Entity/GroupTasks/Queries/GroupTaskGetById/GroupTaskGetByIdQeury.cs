using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetById;

public sealed record GroupTaskGetByIdQeury(Guid Id) : IQuery<GroupTaskResponse>;