using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetByGroupWithBase;

public sealed record GroupTaskStatusGetByGroupIdQuery(
    Guid GroupId) : IQuery<List<GroupTaskStatusRepsonse>>;