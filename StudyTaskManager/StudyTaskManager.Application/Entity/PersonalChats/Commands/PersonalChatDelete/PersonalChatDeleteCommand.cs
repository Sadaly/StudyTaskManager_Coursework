using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatDelete;

public sealed record PersonalChatDeleteCommand(
    Guid PersonalChatId) : DeleteByIdCommand<Domain.Entity.User.Chat.PersonalChat>(PersonalChatId);
