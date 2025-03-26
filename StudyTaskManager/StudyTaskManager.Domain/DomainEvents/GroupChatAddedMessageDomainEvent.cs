using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupChatAddedMessageDomainEvent(Guid GroupChatId) : IDomainEvent;
