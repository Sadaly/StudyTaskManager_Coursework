using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTasks.Queries.GroupTaskGetByGroup;

public sealed record GroupTaskGetAllByGroupIdQuery(Guid GroupId) : IQuery<List<GroupTaskResponse>>;