using StudyTaskManager.Domain.Abstractions;

namespace StudyTaskManager.Domain.DomainEvents;

public sealed record GroupMessageUpdatedDomainEvent(Guid GroupChatId, ulong MessageOrdinal) : IDomainEvent;
