using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserRoleChangedDomainEvent(Guid UserId) : IDomainEvent;
