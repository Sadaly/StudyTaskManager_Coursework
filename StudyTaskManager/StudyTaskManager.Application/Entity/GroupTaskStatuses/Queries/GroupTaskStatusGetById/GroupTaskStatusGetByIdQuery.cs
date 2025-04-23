using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskStatuses.Queries.GroupTaskStatusGetById;

public sealed record GroupTaskStatusGetByIdQuery(Guid Id) : IQuery<GroupTaskStatusRepsonse>;