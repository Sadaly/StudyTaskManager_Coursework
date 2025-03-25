using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    internal static class TableNames
    {
        internal const string GroupChat = nameof(GroupChat);
        internal static class GroupChatTable
        {
            internal const string Title = nameof(Title);
        }
        internal const string GroupChatMessage = nameof(GroupChatMessage);
        internal static class GroupChatMessageTable
        {
            internal const string Context = nameof(Context);
        }
        internal const string GroupChatParticipant = nameof(GroupChatParticipant);
        internal static class GroupChatParticipantTable { }
        internal const string GroupChatParticipantLastRead = nameof(GroupChatParticipantLastRead);
        internal static class GroupChatParticipantLastReadTable { }

        internal const string GroupTask = nameof(GroupTask);
        internal static class GroupTaskTable
        {
            internal const string HeadLine = nameof(HeadLine);
            internal const string Description = nameof(Description);
        }
        internal const string GroupTaskStatus = nameof(GroupTaskStatus);
        internal static class GroupTaskStatusTable
        {
            internal const string Description = nameof(Description);
            internal const string Name = nameof(Name);
        }
        internal const string GroupTaskUpdate = nameof(GroupTaskUpdate);
        internal static class GroupTaskUpdateTable
        {
            internal const string Content = nameof(Content);
        }

        internal const string Group = nameof(Group);
        internal static class GroupTable
        {
            internal const string Title = nameof(Title);
            internal const string Description = nameof(Description);
        }
        internal const string GroupInvite = nameof(GroupInvite);
        internal static class GroupInviteTable { }
        internal const string GroupRole = nameof(GroupRole);
        internal static class GroupRoleTable
        {
            internal const string RoleName = nameof(RoleName);
        }
        internal const string UserInGroup = nameof(UserInGroup);
        internal static class UserInGrouTable { }

        internal const string Log = nameof(Log);
        internal static class LogTable { }
        internal const string LogAction = nameof(LogAction);
        internal static class LogActionTable { }

        internal const string PersonalChat = nameof(PersonalChat);
        internal static class PersonalChatTable { }
        internal const string PersonalMessage = nameof(PersonalMessage);
        internal static class PersonalMessageTable
        {
            internal const string Content = nameof(Content);
        }

        internal const string BlockedUserInfo = nameof(BlockedUserInfo);
        internal static class BlockedUserInfoTable
        {
            internal const string UserId = nameof(UserId);
            internal const string PrevRoleId = nameof(PrevRoleId);
        }

        internal const string SystemRole = nameof(SystemRole);
        internal static class SystemRoleTable
        {
            internal const string Name = nameof(Name);
        }

        internal const string User = nameof(User);
        internal static class UserTable
        {
            internal const string Id = nameof(Id);
            internal const string SystemRoleId = nameof(SystemRoleId);
            internal const string UserName = nameof(UserName);
            internal const string Email = nameof(Email);
            internal const string PhoneNumber = nameof(PhoneNumber);
            internal const string PasswordHash = nameof(PasswordHash);
        }

        internal const string OutboxMessages = nameof(OutboxMessages);

    }
}
