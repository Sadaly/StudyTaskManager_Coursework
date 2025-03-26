using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupMessageCreatedDomainEvent(Guid GroupChatId, ulong MessageOrdinal) : IDomainEvent;
