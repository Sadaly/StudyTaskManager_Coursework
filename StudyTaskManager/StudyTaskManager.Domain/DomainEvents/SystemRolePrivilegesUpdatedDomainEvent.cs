using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record SystemRolePrivilegesUpdatedDomainEvent(Guid SystemRoleId) : IDomainEvent;
