using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserBlockedDomainEvent(Guid UserId) : IDomainEvent;
