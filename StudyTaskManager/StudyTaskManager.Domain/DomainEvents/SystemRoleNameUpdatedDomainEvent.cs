using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record SystemRoleNameUpdatedDomainEvent(Guid SystemRoleId) : IDomainEvent;
