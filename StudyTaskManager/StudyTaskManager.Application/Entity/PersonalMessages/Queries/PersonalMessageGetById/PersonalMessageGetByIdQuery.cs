using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById;

public sealed record PersonalMessageGetByIdQuery(
    Guid PersonalMessageId) : IQuery<PersonalMessageResponse>;
