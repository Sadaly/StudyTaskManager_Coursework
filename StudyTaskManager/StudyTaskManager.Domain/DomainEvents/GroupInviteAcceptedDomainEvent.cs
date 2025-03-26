using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupInviteAcceptedDomainEvent(Guid GroupId, Guid UserId) : IDomainEvent;
