using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById;

public sealed record PersonalMessageGetByIdQuery(
    Guid PersonalMessageId) : IQuery<PersonalMessage>;
