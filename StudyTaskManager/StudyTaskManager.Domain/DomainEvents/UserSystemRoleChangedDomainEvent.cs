using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record UserSystemRoleChangedDomainEvent(Guid UserId) : IDomainEvent;
