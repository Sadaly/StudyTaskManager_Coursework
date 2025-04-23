using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetByGroupAndUser;

public sealed record GroupInviteGetByGroupAndUserQuery(
    Guid ReceiverId,
    Guid GroupId) : IQuery<GroupInviteResponse>;