using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatCreate;

public sealed record PersonalChatCreateCommand(
    Guid User1,
    Guid User2) : ICommand<Guid>;
