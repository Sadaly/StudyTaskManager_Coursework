using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser;

public sealed record PersonalChatsGetByUserQuery(
    Guid UserId) : ICommand<List<PersonalChatsGetByUserResponseElements>>;
