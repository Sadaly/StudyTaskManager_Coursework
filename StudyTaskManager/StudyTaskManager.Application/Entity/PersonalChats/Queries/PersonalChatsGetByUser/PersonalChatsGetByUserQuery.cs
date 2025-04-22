using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetByUser;

public sealed record PersonalChatsGetByUserQuery(
    Guid UserId) : ICommand<List<PersonalChatsGetByUserResponseElements>>;
