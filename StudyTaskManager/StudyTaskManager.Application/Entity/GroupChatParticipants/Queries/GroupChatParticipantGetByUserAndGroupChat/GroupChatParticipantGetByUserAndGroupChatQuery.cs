using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetByUserAndGroupChat;

public sealed record GroupChatParticipantGetByUserAndGroupChatQuery(
    Guid UserId,
    Guid GroupChatId) : IQuery<GroupChatParticipantResponse>;