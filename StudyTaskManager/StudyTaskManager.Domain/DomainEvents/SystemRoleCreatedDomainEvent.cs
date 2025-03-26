using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record SystemRoleCreatedDomainEvent(Guid SystemRoleId) : IDomainEvent;
