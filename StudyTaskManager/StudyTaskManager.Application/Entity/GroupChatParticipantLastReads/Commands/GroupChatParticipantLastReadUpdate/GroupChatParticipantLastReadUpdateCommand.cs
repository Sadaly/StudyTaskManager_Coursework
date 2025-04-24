using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadUpdate;
public sealed record GroupChatParticipantLastReadUpdateCommand(Guid UserId, Guid GroupChatId, ulong NewLastReadId) : ICommand;