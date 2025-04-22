using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup;

public sealed record GroupTaskGetByGroupQuery(Guid GroupId) : IQuery<List<GroupTaskGetByGroupResponseElements>>;