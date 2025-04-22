using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupTaskUpdates.Queries.GroupTaskUpdateGetById;

public sealed record GroupTaskUpdateGetByIdQuery(Guid Id) : IQuery<GroupTaskUpdateGetByIdResponse>;
