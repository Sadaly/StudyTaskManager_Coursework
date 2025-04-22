using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Queries.GroupGetById;

public sealed record GroupGetByIdQuery(Guid Id) : IQuery<GroupGetByIdResponse>;