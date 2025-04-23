using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadCreate;
public sealed record GroupChatParticipantLastReadCreateCommand(Guid UserId, Guid GroupChatId, ulong LastReadId) : ICommand<(Guid, Guid, ulong)>;