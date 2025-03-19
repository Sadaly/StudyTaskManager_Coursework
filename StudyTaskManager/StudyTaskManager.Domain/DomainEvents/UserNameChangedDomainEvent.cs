using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserNameChangedDomainEvent(Guid UserId) : IDomainEvent;
