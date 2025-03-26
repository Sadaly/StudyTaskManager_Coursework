using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record PersonalMessageUpdatedDomainEvent(Guid MessageId) : IDomainEvent;
