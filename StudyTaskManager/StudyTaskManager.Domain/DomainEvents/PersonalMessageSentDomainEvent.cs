using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record PersonalMessageSentDomainEvent(Guid MessageId) : IDomainEvent;
