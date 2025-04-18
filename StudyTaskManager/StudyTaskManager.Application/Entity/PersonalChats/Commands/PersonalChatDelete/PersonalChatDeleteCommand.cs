using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatDelete;

public sealed record PersonalChatDeleteCommand(Guid Id) : ICommand;