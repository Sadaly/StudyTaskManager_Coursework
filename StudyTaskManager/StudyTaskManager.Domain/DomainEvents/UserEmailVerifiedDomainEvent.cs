using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserEmailVerifiedDomainEvent(Guid UserId) : IDomainEvent;
