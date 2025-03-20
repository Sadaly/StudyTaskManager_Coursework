using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupInviteDeclinedDomainEvent(Guid GroupId, Guid UserId) : IDomainEvent;
