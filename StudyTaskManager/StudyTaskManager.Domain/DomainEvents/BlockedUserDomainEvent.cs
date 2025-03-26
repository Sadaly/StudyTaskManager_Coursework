using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record BlockedUserDomainEvent(Guid UserId) : IDomainEvent;
