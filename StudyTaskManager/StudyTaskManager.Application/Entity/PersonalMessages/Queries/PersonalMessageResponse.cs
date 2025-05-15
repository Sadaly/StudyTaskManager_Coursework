using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries;
public sealed record PersonalMessageResponse(
    Guid MessageId,
    Guid PersonalChatId,
    Guid SenderId,
    DateTime DateWriten,
    string Content,
    bool Is_Read_By_Other_User)
{
    internal PersonalMessageResponse(PersonalMessage message)
        : this(
              message.Id,
              message.PersonalChatId,
              message.SenderId,
              message.DateWriten,
              message.Content.Value,
              message.Is_Read_By_Other_User)
    { }
}
