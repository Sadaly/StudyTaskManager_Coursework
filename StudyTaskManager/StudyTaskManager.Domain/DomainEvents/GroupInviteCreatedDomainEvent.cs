using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupInviteCreatedDomainEvent(Guid GroupId, Guid UserId) : IDomainEvent;
