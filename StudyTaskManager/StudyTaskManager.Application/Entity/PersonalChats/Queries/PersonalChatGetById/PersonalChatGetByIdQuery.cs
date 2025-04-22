using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById;

public sealed record PersonalChatGetByIdQuery(Guid IdPersonalChat) : IQuery<PersonalChatGetByIdResponse>;