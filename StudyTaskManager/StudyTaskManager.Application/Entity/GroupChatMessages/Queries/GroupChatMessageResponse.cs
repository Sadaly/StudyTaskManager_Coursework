using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries;
public sealed record GroupChatMessageResponse(GroupChatMessage GroupChatMessage);
public sealed record GroupChatMessageListResponse(List<GroupChatMessage> GroupChatMessageList);