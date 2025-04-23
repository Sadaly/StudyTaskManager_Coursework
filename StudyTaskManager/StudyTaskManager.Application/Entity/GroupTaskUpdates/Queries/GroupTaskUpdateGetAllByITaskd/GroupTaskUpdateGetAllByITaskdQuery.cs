using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetByITaskd;

public sealed record GroupTaskUpdateGetAllByITaskdQuery(
    Guid TaskId,
    int StartIndex,
    int Count) : IQuery<List<GroupTaskUpdateResponse>>;