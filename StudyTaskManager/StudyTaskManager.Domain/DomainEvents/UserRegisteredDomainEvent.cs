using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserRegisteredDomainEvent(Guid MemberId) : IDomainEvent;
