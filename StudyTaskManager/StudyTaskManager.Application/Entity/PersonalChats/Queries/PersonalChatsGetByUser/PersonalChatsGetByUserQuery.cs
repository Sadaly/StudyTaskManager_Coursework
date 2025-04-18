using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetByUser;

public sealed record PersonalChatsGetByUserQuery(
    Guid UserId) : ICommand<List<PersonalChat>>;
