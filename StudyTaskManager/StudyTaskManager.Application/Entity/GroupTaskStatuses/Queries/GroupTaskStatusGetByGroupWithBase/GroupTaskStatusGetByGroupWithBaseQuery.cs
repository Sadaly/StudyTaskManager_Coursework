using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetByGroupWithBase;

public sealed record GroupTaskStatusGetByGroupWithBaseQuery(
    Guid GroupId,
    int StartIndex,
    int Count) : IQuery<List<GroupTaskStatusGetByGroupWithBaseRepsonseElements>>;