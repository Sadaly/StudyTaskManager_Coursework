using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatCreatedDomainEvent(Guid GroupChatId) : IDomainEvent;
