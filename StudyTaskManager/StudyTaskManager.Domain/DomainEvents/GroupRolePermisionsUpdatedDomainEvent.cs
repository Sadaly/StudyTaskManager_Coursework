using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupRolePermisionsUpdatedDomainEvent(Guid RoleId) : IDomainEvent;
