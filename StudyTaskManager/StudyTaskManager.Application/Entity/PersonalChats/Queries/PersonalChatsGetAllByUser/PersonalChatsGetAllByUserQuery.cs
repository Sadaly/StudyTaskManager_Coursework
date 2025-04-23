using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser;

public sealed record PersonalChatsGetAllByUserQuery(
    Guid UserId) : ICommand<List<PersonalChatsResponse>>;
