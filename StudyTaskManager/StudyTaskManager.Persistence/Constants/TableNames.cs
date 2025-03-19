namespace StudyTaskManager.Persistence.Constants
{
    internal static class TableNames
    {
        internal const string GroupChat = nameof(GroupChat);
        internal const string GroupChatMessage = nameof(GroupChatMessage);
        internal const string GroupChatParticipant = nameof(GroupChatParticipant);
        internal const string GroupChatParticipantLastRead = nameof(GroupChatParticipantLastRead);

        internal const string GroupTask = nameof(GroupTask);
        internal const string GroupTaskStatus = nameof(GroupTaskStatus);
        internal const string GroupTaskUpdate = nameof(GroupTaskUpdate);

        internal const string Group = nameof(Group);
        internal const string GroupInvite = nameof(GroupInvite);
        internal const string GroupRole = nameof(GroupRole);
        internal const string UserInGroup = nameof(UserInGroup);

        internal const string Log = nameof(Log);
        internal const string LogAction = nameof(LogAction);

        internal const string PersonalChat = nameof(PersonalChat);
        internal const string PersonalMessage = nameof(PersonalMessage);
        internal const string BlockedUserInfo = nameof(BlockedUserInfo);

        internal const string SystemRole = nameof(SystemRole);
        internal const string Users = nameof(Users);
    }
}
